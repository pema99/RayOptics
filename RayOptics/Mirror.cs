using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public class Mirror : Manipulator
    {
        public Mirror(Vector A, Vector B)
            : base(A, B)
        {
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawLine(A.X, A.Y, B.X, B.Y, Config.MirrorThickness, Config.MirrorColor);
        }
    }
}
