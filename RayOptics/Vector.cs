using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Magnitude { get { return Math.Sqrt(X * X + Y * Y); } }
        public Vector Normalized { get { return this / Magnitude; } }

        public Vector(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public static Vector operator + (Vector A, Vector B)
        {
            return new Vector(A.X + B.X, A.Y + B.Y);
        }

        public static Vector operator - (Vector A, Vector B)
        {
            return new Vector(A.X - B.X, A.Y - B.Y);
        }

        public static Vector operator / (Vector A, double B)
        {
            return new Vector(A.X / B, A.Y / B);
        }

        public static Vector operator * (Vector A, double B)
        {
            return new Vector(A.X * B, A.Y * B);
        }

        public static Vector operator * (double A, Vector B)
        {
            return new Vector(B.X * A, B.Y * A);
        }
    }
}
