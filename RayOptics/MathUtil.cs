using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public static class MathUtil
    {
        public static double Cross(double Vx, double Vy, double Wx, double Wy)
        {
            return Vx * Wy - Vy * Wx;
        }

        public static double Cross(Vector V, Vector W)
        {
            return Cross(V.X, V.Y, W.X, W.Y);
        }

        public static double Dot(double Vx, double Vy, double Wx, double Wy)
        {
            return Vx * Wx + Vy * Wy;
        }

        public static double Dot(Vector V, Vector W)
        {
            return Dot(V.X, V.Y, W.X, W.Y);
        }

        public static double Magnitude(double Vx, double Vy)
        {
            return Math.Sqrt(Vx * Vx + Vy * Vy);
        }

        public static double Magnitude(Vector V)
        {
            return V.Magnitude;
        }
    }
}
