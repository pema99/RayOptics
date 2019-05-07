using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RayOptics
{
    public class LineMirror : Manipulator
    {
        public Vec A { get; set; }
        public Vec B { get; set; }

        public LineMirror(Vec a, Vec B)
        {
            this.A = a;
            this.B = B;
        }

        public override Ray HandleIntersection(Ray ray)
        {
            Vec lineVec = B - A;
            Vec dirVec = ray.Direction * ray.Length;
            double lineCrossDir = lineVec.Cross(dirVec);

            if (lineCrossDir != 0)
            {
                Vec rayToA = ray.Origin - A;

                double t = rayToA.Cross(dirVec) / lineCrossDir;
                double u = rayToA.Cross(lineVec) / lineCrossDir;

                Vec hit = A + lineVec * t;

                if (0 <= t && t <= 1 && 0 <= u && u <= 1)
                {
                    Vec normal = new Vec((B.Y - A.Y), -(B.X - A.X)).Unit();
                    Vec reflVec = dirVec - 2 * (dirVec.X * normal.X + dirVec.Y * normal.Y) * normal;
                    return new Ray(hit + reflVec * 0.001, reflVec);
                }
            }

            return null;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(A.X, A.Y, B.X, B.Y, 2, Color.Blue);
        }
    }
}
