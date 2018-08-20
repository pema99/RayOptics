using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayOptics
{
    public class SingleRay
    {
        public Vector Position { get; set; }
        public double Angle { get; set; }
        public Vector Vector { get { return new Vector(Math.Cos(Angle) * Config.RayLength, Math.Sin(Angle) * Config.RayLength); } }
        public Vector Direction { get { return new Vector(Math.Cos(Angle), Math.Sin(Angle)); } }
        public List<RaySegment> Segments { get; set; }

        public SingleRay(Vector Position, double Angle)
        {
            this.Position = Position;
            this.Angle = Angle;
            this.Segments = new List<RaySegment>();
        }

        public void Update(GameTime gameTime)
        {
            Angle = Angle % (2*Math.PI);
            if (Angle < 0)
            {
                this.Angle += (2*Math.PI);
            }

            Segments = new List<RaySegment>();
            Propagate(Position, Direction, null, false);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (RaySegment Segment in Segments)
            {
                Segment.Draw(spriteBatch);
            }
        }

        public void Propagate(Vector Position, Vector Direction, Manipulator PreviousObject, bool InMaterial)
        {
            List<Tuple<Vector, Manipulator>> Hits = new List<Tuple<Vector, Manipulator>>();
            Vector Vector = Direction * Config.RayLength;

            //Raycast to all Mirrors
            foreach (Mirror Mirror in World.Mirrors)
            {
                if (Mirror == PreviousObject)
                {
                    continue;
                }

                double T = MathUtil.Cross(Position - Mirror.A, Vector) / MathUtil.Cross(Mirror.B - Mirror.A, Vector);
                double U = MathUtil.Cross(Position - Mirror.A, Mirror.B - Mirror.A) / MathUtil.Cross(Mirror.B - Mirror.A, Vector);

                Vector Hit = Mirror.A + (Mirror.B - Mirror.A) * T;

                if (MathUtil.Cross(Mirror.B - Mirror.A, Vector) != 0 && 0 <= T && T <= 1 && 0 <= U && U <= 1)
                {
                    Hits.Add(Tuple.Create(Hit, (Manipulator)Mirror));
                }
            }

            //Raycast to all GlassPolygons
            foreach (GlassPolygon GlassPolygon in World.GlassPolygons)
            {
                foreach (GlassPane GlassPane in GlassPolygon.Edges)
                {
                    if (GlassPane == PreviousObject)
                    {
                        continue;
                    }

                    double T = MathUtil.Cross(Position - GlassPane.A, Vector) / MathUtil.Cross(GlassPane.B - GlassPane.A, Vector);
                    double U = MathUtil.Cross(Position - GlassPane.A, GlassPane.B - GlassPane.A) / MathUtil.Cross(GlassPane.B - GlassPane.A, Vector);

                    Vector Hit = GlassPane.A + (GlassPane.B - GlassPane.A) * T;

                    if (MathUtil.Cross(GlassPane.B - GlassPane.A, Vector) != 0 && 0 <= T && T <= 1 && 0 <= U && U <= 1)
                    {
                        Hits.Add(Tuple.Create(Hit, (Manipulator)GlassPane));
                    }
                }
            }

            //If not hits, continue ray
            if (Hits.Count == 0)
            {
                Segments.Add(new RaySegment(Position, Position + Vector));
            }

            //If any hits, find closest one and handle it
            else
            {
                Tuple<Vector, Manipulator> Closest = Hits.OrderBy(x => (x.Item1 - Position).Magnitude).First();

                //If hit Mirror, reflection
                if (Closest.Item2 is Mirror)
                {
                    Mirror Hit = Closest.Item2 as Mirror;
                    Vector Normal = new Vector((Hit.B.Y - Hit.A.Y), -(Hit.B.X - Hit.A.X));
                    Normal = Normal.Normalized;

                    Vector Reflection = Direction - 2 * (Direction.X * Normal.X + Direction.Y * Normal.Y) * Normal;

                    Segments.Add(new RaySegment(Position, Closest.Item1));
                    Propagate(Closest.Item1, Reflection, Hit, InMaterial);
                    return;
                }

                //If hit GlassPane, refraction
                if (Closest.Item2 is GlassPane)
                {
                    GlassPane Hit = Closest.Item2 as GlassPane;
                    Vector Normal = new Vector((Hit.B.Y - Hit.A.Y), -(Hit.B.X - Hit.A.X));
                    Normal = Normal.Normalized;

                    //Handle going in and out of materials
                    double OutAngle = 0;
                    double IndexA = 0;
                    double IndexB = 0;
                    if (InMaterial)
                    {
                        IndexA = Hit.RefractiveIndex;
                        IndexB = 1;
                    }
                    else
                    {
                        IndexA = 1;
                        IndexB = Hit.RefractiveIndex;
                    }

                    //If flipped
                    if (MathUtil.Dot(Normal, Direction) < 0)
                    {
                        Normal.X = -Normal.X;
                        Normal.Y = -Normal.Y;

                        double InAngle = 
                            Math.Acos(MathUtil.Dot(Closest.Item1 - Position, Normal) 
                            / MathUtil.Magnitude(Closest.Item1 - Position) 
                            * MathUtil.Magnitude(Normal));

                        double SinValue = Math.Sin(InAngle) * IndexA / IndexB;
                        
                        //Total inner reflection
                        if (SinValue > 1)
                        {
                            Vector Reflection = Direction - 2 * (Direction.X * Normal.X + Direction.Y * Normal.Y) * Normal;
                            OutAngle = Math.Atan2(Reflection.Y, Reflection.X);
                        }
                        //Refraction
                        else
                        {
                            InMaterial = !InMaterial;
                            OutAngle = Math.Asin(SinValue);

                            if (MathUtil.Dot(Hit.B - Hit.A, Closest.Item1 - Position) > 0)
                            {
                                OutAngle = Math.Atan2(Normal.Y, Normal.X) - OutAngle;
                            }
                            else
                            {
                                OutAngle += Math.Atan2(Normal.Y, Normal.X);
                            }
                        }
                    }
                    else
                    {
                        double InAngle =
                            Math.Acos(MathUtil.Dot(Closest.Item1 - Position, Normal)
                            / MathUtil.Magnitude(Closest.Item1 - Position)
                            * MathUtil.Magnitude(Normal));

                        double SinValue = Math.Sin(InAngle) * IndexA / IndexB;
                        //Total inner reflection
                        if (SinValue > 1)
                        {
                            Vector Reflection = Direction - 2 * (Direction.X * Normal.X + Direction.Y * Normal.Y) * Normal;
                            OutAngle = Math.Atan2(Reflection.Y, Reflection.X);
                        }
                        //Refraction
                        else
                        {
                            InMaterial = !InMaterial;
                            OutAngle = Math.Asin(SinValue);
                            if (MathUtil.Dot(Hit.B - Hit.A, Closest.Item1 - Position) > 0)
                            {
                                OutAngle += Math.Atan2(Normal.Y, Normal.X);
                            }
                            else
                            {
                                OutAngle = Math.Atan2(Normal.Y, Normal.X) - OutAngle;
                            }
                        }
                    }

                    Segments.Add(new RaySegment(Position, Closest.Item1));
                    Propagate(Closest.Item1, new Vector(Math.Cos(OutAngle), Math.Sin(OutAngle)), Hit, InMaterial);
                }
            }
        }
    }
}
