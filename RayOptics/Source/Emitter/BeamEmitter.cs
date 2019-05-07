using System;

namespace RayOptics
{
    public class BeamEmitter : Emitter
    {
        public Vec A { get; set; }
        public Vec B { get; set; }
        public int NumRays { get; set; }

        public BeamEmitter(Vec a, Vec b, int numRays)
        {
            
        }
    }
}
