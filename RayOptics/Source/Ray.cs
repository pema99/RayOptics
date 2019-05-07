using System;

namespace RayOptics
{
    public class Ray
    {
        private const double MAX_RAY_LENGTH = 1000;

        public Vec Origin { get; set; }
        public Vec Direction { get; set; }
        public double Length { get; set; }

        public Ray(Vec origin, Vec direction, double length = MAX_RAY_LENGTH)
        {
            this.Origin = origin;
            this.Direction = direction.Unit();
            this.Length = length;
        }
    }
}
