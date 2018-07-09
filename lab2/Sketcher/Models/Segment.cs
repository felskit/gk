using System;
using Sketcher.Helpers;

namespace Sketcher.Models
{
    public class Segment
    {
        private const int Tolerance = 4;

        public Vertex From { get; set; }
        public Vertex To { get; set; }

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
            if (!OnRectangle(v, new Vertex(xmin - Tolerance, ymin - Tolerance), new Vertex(xmax + Tolerance, ymax + Tolerance))) return false;

            var num = Math.Abs((To.Y - From.Y) * v.X - (To.X - From.X) * v.Y + To.X * From.Y - To.Y * From.X);
            var denom = (To.Y - From.Y) * (To.Y - From.Y) + (To.X - From.X) * (To.X - From.X);
            return num / Math.Sqrt(denom) <= Tolerance;
        }

        public bool Intersects(Segment s)
        {
            if (this == s || From == s.To || To == s.From) return false;

            var p43 = s.To - s.From;
            var p13 = From - s.From;
            var p23 = To - s.From;
            var p21 = To - From;
            var p31 = s.From - From;
            var p41 = s.To - From;

            var d1 = p43 * p13;
            var d2 = p43 * p23;
            var d3 = p21 * p31;
            var d4 = p21 * p41;

            var d12 = Math.Sign(d1) * Math.Sign(d2);
            var d34 = Math.Sign(d3) * Math.Sign(d4);

            if (d12 > 0 || d34 > 0) return false;
            if (d12 < 0 || d34 < 0) return true;

            return OnRectangle(From, s.From, s.To) || OnRectangle(To, s.From, s.To) ||
                   OnRectangle(s.From, From, To) || OnRectangle(s.To, From, To);
        }

        public Vertex Intersection(Segment s)
        {
            int x1 = From.X, x2 = To.X,
                y1 = From.Y, y2 = To.Y,
                x3 = s.From.X, x4 = s.To.X,
                y3 = s.From.Y, y4 = s.To.Y;

            if (x1 == x2 || x3 == x4)
            {
                if (x3 == x4)
                {
                    General.Swap(ref x1, ref x3);
                    General.Swap(ref x2, ref x4);
                    General.Swap(ref y1, ref y3);
                    General.Swap(ref y2, ref y4);
                }

                var av = (double)(y4 - y3) / (x4 - x3);
                var bv = y3 - av * x3;
                var yv = av * x1 + bv;

                return new Vertex(x1, (int)Math.Round(yv));
            }

            var a1 = (double)(y2 - y1) / (x2 - x1);
            var a2 = (double)(y4 - y3) / (x4 - x3);
            var b1 = y1 - a1 * x1;
            var b2 = y3 - a2 * x3;

            var x0 = (b2 - b1) / (a1 - a2);
            var y0 = a1 * x0 + b1;

            return new Vertex((int)Math.Round(x0), (int)Math.Round(y0));
        }

        public void Reverse()
        {
            var tmp = From;
            From = To;
            To = tmp;
        }

        private static bool OnRectangle(Vertex v, Vertex p1, Vertex p2)
        {
            return Math.Min(p1.X, p2.X) <= v.X && v.X <= Math.Max(p1.X, p2.X) &&
                   Math.Min(p1.Y, p2.Y) <= v.Y && v.Y <= Math.Max(p1.Y, p2.Y);
        }
    }
}
