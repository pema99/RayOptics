using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace RayOptics
{
    public class World
    {
        public const double MAX_RAY_LENGTH = 1000;

        private List<Ray> raySegments;

        public List<Emitter> Emitters { get; private set; }
        public List<Manipulator> Manipulators { get; private set; }

        public World()
        {
            this.raySegments = new List<Ray>();

            this.Emitters = new List<Emitter>();
            this.Emitters.Add(new SingleEmitter(new Ray(new Vec(30, 10), new Vec(1, 1))));

            this.Manipulators = new List<Manipulator>();
            //this.Manipulators.Add(new LineMirror(new Vec(100, 0), new Vec(100, 100)));
            this.Manipulators.Add(new LineMirror(new Vec(20, 100), new Vec(70, 150)));
            this.Manipulators.Add(new CircleMirror(new Vec(150, 50), 50));
        }

        public void Update(GameTime gameTime)
        {
            raySegments.Clear();
            foreach (Emitter emitter in Emitters)
            {
                foreach (Ray ray in emitter.Sources)
                {
                    PropagateRay(ray);
                }
            }

            Emitters[0].Sources[0].Direction = 
                new Vec(Mouse.GetState().X - Emitters[0].Sources[0].Origin.X, 
                Mouse.GetState().Y - Emitters[0].Sources[0].Origin.Y).Unit();
        }

        public void PropagateRay(Ray ray)
        {
            List<Ray> hits = new List<Ray>();

            //Check everything for intersection
            foreach (Manipulator m in Manipulators)
            {
                Ray next = m.HandleIntersection(ray);
                if (next != null)
                {
                    hits.Add(next);
                }
            }

            //If nothing found continue ray
            if (hits.Count == 0)
            {
                raySegments.Add(ray);
                return;
            }

            //Find closest intersection
            Ray closestHit = null;
            double closestDist = double.MaxValue;
            foreach (Ray hit in hits)
            {
                Vec toHit = (hit.Origin - ray.Origin);
                double dist = toHit.Length();
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestHit = hit;
                }
            }

            //Propagate next ray
            raySegments.Add(new Ray(ray.Origin, ray.Direction, closestDist));
            PropagateRay(closestHit);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Ray ray in raySegments)
            {
                Vec to = ray.Origin + ray.Direction * ray.Length;
                spriteBatch.DrawLine(ray.Origin.X, ray.Origin.Y, to.X, to.Y, 2, Color.Red);
            }

            foreach (Manipulator m in Manipulators)
            {
                m.Draw(spriteBatch);
            }
        }
    }
}
