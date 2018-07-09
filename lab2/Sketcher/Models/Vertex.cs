using System;

namespace Sketcher.Models
{
    public class Vertex
    {
        public int X { get; set; }
        public int Y { get; set; }

        public static readonly int Size = 7;

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsEqualTo(Vertex v)
        {
            return Math.Abs(X - v.X) < Size / 2 + 1 && Math.Abs(Y - v.Y) < Size / 2 + 1;
        }

        public static Vertex Average(Vertex v1, Vertex v2)
        {
            return new Vertex((v1.X + v2.X) / 2, (v1.Y + v2.Y) / 2);
        }

        public static int DistSquared(Vertex v1, Vertex v2)
        {
            var dX = v2.X - v1.X;
            var dY = v2.Y - v1.Y;
            return dX * dX + dY * dY;
        }

        public static Vertex operator -(Vertex v1, Vertex v2)
        {
            return new Vertex(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static int operator *(Vertex v1, Vertex v2)
        {
            return v1.X * v2.Y - v2.X * v1.Y;
        }
    }
}
