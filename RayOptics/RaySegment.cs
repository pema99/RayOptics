using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public class RaySegment
    {
        public Vector A { get; set; }
        public Vector B { get; set; }

        public RaySegment(Vector A, Vector B)
        {
            this.A = A;
            this.B = B;
        }

        public RaySegment(Vector A, double Angle)
        {
            this.A = A;
            this.B = new Vector(Math.Cos(Angle) * Config.RayLength, Math.Sin(Angle) * Config.RayLength);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(A.X, A.Y, B.X, B.Y, Config.RayThickness, Config.RayColor);
        }
    }
}
