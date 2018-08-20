using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public static class World
    {
        public static List<SingleRay> SingleRays = new List<SingleRay>() { new SingleRay(new Vector(20, 50), 0.785) };
        public static List<Mirror> Mirrors = new List<Mirror>() { new Mirror(new Vector(168, 252), new Vector(458, 179)), new Mirror(new Vector(218, 130), new Vector(308, 100)) };
        public static List<GlassPolygon> GlassPolygons = new List<GlassPolygon>() { new GlassPolygon(new List<Vector>() { new Vector(70, 10), new Vector(70, 80), new Vector(120, 80), new Vector(120, 10) }, 2.5) };

        static World()
        {
            for (int i = -100; i < 330 - 100; i += 5)
            {
                Mirrors.Add
                (
                    new Mirror(
                        new Vector(Math.Round(Math.Cos(MathHelper.ToRadians(i)) * 50 + 170), Math.Round(Math.Sin(MathHelper.ToRadians(i)) * 50 + 370)),
                        new Vector(Math.Round(Math.Cos(MathHelper.ToRadians(i + 5)) * 50 + 170), Math.Round(Math.Sin(MathHelper.ToRadians(i + 5)) * 50 + 370))
                    )
                );
            }

            List<Vector> Vertices = new List<Vector>();
            for (int i = 0; i < 360; i+=5)
            {
                Vertices.Add(new Vector(Math.Round(Math.Cos(MathHelper.ToRadians(i))*50+60), Math.Round(Math.Sin(MathHelper.ToRadians(i))*50+370)));
            }
            GlassPolygons.Add(new GlassPolygon(Vertices, 1.5));
        }

        public static void Update(GameTime gameTime)
        {
            foreach (SingleRay Ray in SingleRays)
            {
                Ray.Update(gameTime);
            }
            foreach (Mirror Mirror in Mirrors)
            {
                Mirror.Update(gameTime);
            }
            foreach (GlassPolygon GlassPolygon in GlassPolygons)
            {
                GlassPolygon.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                SingleRays[0].Angle -= 0.01;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                SingleRays[0].Angle += 0.01;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (SingleRay Ray in SingleRays)
            {
                Ray.Draw(spriteBatch);
            }
            foreach (Mirror Mirror in Mirrors)
            {
                Mirror.Draw(spriteBatch);
            }
            foreach (GlassPolygon GlassPolygon in GlassPolygons)
            {
                GlassPolygon.Draw(spriteBatch);
            }
        }
    }
}
