using System;

namespace RayOptics
{
    public class SingleEmitter : Emitter
    {
        public SingleEmitter(Ray source)
        {
            this.Sources.Add(source);
        }
    }
}
