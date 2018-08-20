using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public class GlassPane : Manipulator
    {          
        private GlassPolygon Parent { get; set; }
        public double RefractiveIndex { get { return Parent.RefractiveIndex; } }

        public GlassPane(Vector A, Vector B, GlassPolygon Parent)
            : base(A, B)
        {
            this.Parent = Parent; 
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(A.X, A.Y, B.X, B.Y, Config.GlassThickness, Config.GlassColor);
        }
    }
}
