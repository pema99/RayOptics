using Microsoft.Xna.Framework.Graphics;
using System;

namespace RayOptics
{
    public abstract class Manipulator
    {
        public abstract Ray HandleIntersection(Ray ray);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
