using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RayOptics
{
    public class CircleMirror : Manipulator
    {
        public Vec Origin { get; set; }
        public int Radius { get; set; }

        public CircleMirror(Vec origin, int radius)
        {
            this.Origin = origin;
            this.Radius = radius;
        }

        public override Ray HandleIntersection(Ray ray)
        {
            //throw new NotImplementedException();
            return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(Origin.X, Origin.Y, Radius, 100, 2, Color.Blue);
        }
    }
}
