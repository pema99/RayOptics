using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public static class DrawUtil
    {
        public static void DrawLine(this SpriteBatch spriteBatch, double Ax, double Ay, double Bx, double By, int Thickness, Color Tint)
        {
            spriteBatch.Draw(Game1.Blank, new Rectangle((int)Math.Round(Ax), (int)Math.Round(Ay), (int)Math.Round(Vector2.Distance(new Vector2((float)Ax, (float)Ay), new Vector2((float)Bx, (float)By))), Thickness), null, Tint, (float)Math.Atan2(By - Ay, Bx - Ax), new Vector2(0, 0.5f), SpriteEffects.None, 0);
        }

        public static void DrawPoint(this SpriteBatch spriteBatch, double Ax, double Ay, int Thickness, Color Tint)
        {
            spriteBatch.Draw(Game1.Blank, new Rectangle((int)Ax-Thickness/2, (int)Ay-Thickness/2, Thickness, Thickness), Tint);
        }
    }
}
