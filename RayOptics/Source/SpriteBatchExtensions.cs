using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public static class SpriteBatchExtensions
    {
        private static Texture2D blank;

        private static void InitBlank(GraphicsDevice g)
        {
            if (blank == null)
            {
                blank = new Texture2D(g, 1, 1);
                blank.SetData(new Color[] { Color.White });
            }
        }

        public static void DrawLine(this SpriteBatch spriteBatch, double ax, double ay, double bx, double by, int thickness, Color tint)
        {
            InitBlank(spriteBatch.GraphicsDevice);
            spriteBatch.Draw(blank, new Rectangle((int)ax, (int)ay, (int)Math.Ceiling((new Vec(ax, ay) - new Vec(bx, by)).Length()), thickness), null, tint, (float)Math.Atan2(by - ay, bx - ax), new Vector2(0, 0.5f), SpriteEffects.None, 0);
        }

        public static void DrawPoint(this SpriteBatch spriteBatch, double ax, double ay, int thickness, Color tint)
        {
            InitBlank(spriteBatch.GraphicsDevice);
            spriteBatch.Draw(blank, new Rectangle((int)ax - thickness / 2, (int)ay - thickness / 2, thickness, thickness), tint);
        }

        public static void DrawArc(this SpriteBatch spriteBatch, double cx, double cy, double radius, double startAngle, double endAngle, int subdivision, int thickness, Color tint)
        {
            double curAngle = (startAngle + endAngle) / subdivision;
            for (int i = 0; i < subdivision+1; i++)
            {
                double nextAngle = (startAngle + endAngle) / subdivision * (i + 1);
                spriteBatch.DrawLine(cx + Math.Cos(curAngle) * radius, cy + Math.Sin(curAngle) * radius, cx + Math.Cos(nextAngle) * radius, cy + Math.Sin(nextAngle) * radius, thickness, tint);
                curAngle = nextAngle;
            }
        }

        public static void DrawCircle(this SpriteBatch spriteBatch, double cx, double cy, double radius, int subdivision, int thickness, Color tint)
        {
            spriteBatch.DrawArc(cx, cy, radius, 0, Math.PI * 2, subdivision, thickness, tint);
        }
    }
}
