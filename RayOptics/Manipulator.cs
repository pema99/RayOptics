using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public class Manipulator
    {
        public Vector A { get; set; }
        public Vector B { get; set; }

        public Manipulator(Vector A, Vector B)
        {
            this.A = A;
            this.B = B;
        }
    }
}
