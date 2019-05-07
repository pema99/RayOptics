using System.Collections.Generic;

namespace RayOptics
{
    public abstract class Emitter
    {
        public List<Ray> Sources { get; protected set; }

        public Emitter()
        {
            Sources = new List<Ray>();
        }
    }
}
