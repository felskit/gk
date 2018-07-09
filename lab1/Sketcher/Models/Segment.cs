using System;
using Sketcher.Models.Constraints;

namespace Sketcher.Models
{
    public class Segment
    {
        private const int Eps = 4;

        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public IConstraint Constraint { get; set; }
        public bool Antialiased { get; set; }

        public int Length
        {
            get
            {
                var dX = To.X - From.X;
                var dY = To.Y - From.Y;
                return (int)Math.Sqrt(dX * dX + dY * dY);
            }
        }

        public Segment(Vertex from, Vertex to)
        {
            From = from;
            To = to;
        }

        public bool IsClicked(Vertex v)
        {
            var xmin = Math.Min(From.X, To.X);
            var ymin = Math.Min(From.Y, To.Y);
            var xmax = Math.Max(From.X, To.X);
            var ymax = Math.Max(From.Y, To.Y);
            if (!OnRectangle(v, new Vertex(xmin - Eps, ymin - Eps), new Vertex(xmax + Eps, ymax + Eps))) return false;

            var num = Math.Abs((To.Y - From.Y) * v.X - (To.X - From.X) * v.Y + To.X * From.Y - To.Y * From.X);
            var denom = (To.Y - From.Y) * (To.Y - From.Y) + (To.X - From.X) * (To.X - From.X);
            return num / Math.Sqrt(denom) <= Eps;
        }

        public bool Intersects(Segment s)
        {
            var p43 = s.To - s.From;
            var p13 = From - s.From;
            var p23 = To - s.From;
            var p21 = To - From;
            var p31 = s.From - From;
            var p41 = s.To - From;

            var d1 = CrossProduct(p43, p13);
            var d2 = CrossProduct(p43, p23);
            var d3 = CrossProduct(p21, p31);
            var d4 = CrossProduct(p21, p41);

            var d12 = Math.Sign(d1) * Math.Sign(d2);
            var d34 = Math.Sign(d3) * Math.Sign(d4);

            if (d12 > 0 || d34 > 0) return false;
            if (d12 < 0 || d34 < 0) return true;

            return OnRectangle(From, s.From, s.To) || OnRectangle(To, s.From, s.To) ||
                   OnRectangle(s.From, From, To) || OnRectangle(s.To, From, To);
        }

        private static int CrossProduct(Vertex v1, Vertex v2)
        {
            return v1.X * v2.Y - v2.X * v1.Y;
        }

        private static bool OnRectangle(Vertex v, Vertex p1, Vertex p2)
        {
            return Math.Min(p1.X, p2.X) <= v.X && v.X <= Math.Max(p1.X, p2.X) &&
                   Math.Min(p1.Y, p2.Y) <= v.Y && v.Y <= Math.Max(p1.Y, p2.Y);
        }
    }
}
