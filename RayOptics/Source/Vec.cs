using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public class Vec
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vec(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double LengthSquared()
        {
            return X * X + Y * Y;
        }

        public Vec Unit()
        {
            return this / Length();
        }

        public double Cross(Vec other)
        {
            return this.X * other.Y - this.Y * other.X;
        }

        public double Dot(Vec other)
        {
            return this.X * other.X + this.Y * other.Y;
        }

        public static Vec operator + (Vec a, Vec b)
        {
            return new Vec(a.X + b.X, a.Y + b.Y);
        }

        public static Vec operator - (Vec a, Vec b)
        {
            return new Vec(a.X - b.X, a.Y - b.Y);
        }

        public static Vec operator / (Vec a, double b)
        {
            return new Vec(a.X / b, a.Y / b);
        }

        public static Vec operator * (Vec a, double b)
        {
            return new Vec(a.X * b, a.Y * b);
        }

        public static Vec operator * (double a, Vec b)
        {
            return new Vec(b.X * a, b.Y * a);
        }
    }
}
