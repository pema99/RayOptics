using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public class GlassPolygon
    {
        private List<Vector> _Vertices;
        public List<Vector> Vertices { get { return _Vertices; } set { _Vertices = value; RecalculateEdges(); } }
        public List<GlassPane> Edges { get; private set; }
        public double RefractiveIndex { get; set; }

        public GlassPolygon(List<Vector> Vertices, double RefractiveIndex)
        {
            this.Vertices = Vertices;
            this.RefractiveIndex = RefractiveIndex;
        }

        public void Update(GameTime gameTime)
        {
            foreach (GlassPane Glass in Edges)
            {
                Glass.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GlassPane Glass in Edges)
            {
                Glass.Draw(spriteBatch);
            }
        }

        public void RecalculateEdges()
        {
            Edges = new List<GlassPane>();
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vector A = Vertices[i];
                Vector B = Vertices[(i + 1) % Vertices.Count];
                Edges.Add(new GlassPane(A, B, this));
            }
        }
    }
}
