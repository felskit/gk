using Sketcher.Models;
using System.Collections.Generic;

namespace Sketcher.Helpers
{
    public static class General
    {
        public static void Swap<T>(ref T o1, ref T o2)
        {
            var tmp = o1;
            o1 = o2;
            o2 = tmp;
        }

        public static int Mod(int x, int m)
        {
            var r = x % m;
            return r < 0 ? r + m : r;
        }

        public static void PopulatePolygons(LinkedList<Polygon> polygons, int width, int height)
        {
            var p1 = new Polygon();
            p1.Vertices.AddLast(new Vertex(width / 4, height / 2 - 20));
            p1.Vertices.AddLast(new Vertex(width / 4 * 3, height / 2 - 20));
            p1.Vertices.AddLast(new Vertex(width / 4 * 3, height / 2 + 20));
            p1.Vertices.AddLast(new Vertex(width / 4, height / 2 + 20));
            p1.CreateSegments();
            polygons.AddLast(p1);

            var p2 = new Polygon();
            p2.Vertices.AddLast(new Vertex(width / 2 - 20, height / 4));
            p2.Vertices.AddLast(new Vertex(width / 2 + 20, height / 4));
            p2.Vertices.AddLast(new Vertex(width / 2 + 20, height / 4 * 3));
            p2.Vertices.AddLast(new Vertex(width / 2 - 20, height / 4 * 3));
            p2.CreateSegments();
            polygons.AddLast(p2);

            var p3 = new Polygon();
            p3.Vertices.AddLast(new Vertex(10, 10));
            p3.Vertices.AddLast(new Vertex(width - 10, 10));
            p3.Vertices.AddLast(new Vertex(width - 10, height - 10));
            p3.Vertices.AddLast(new Vertex(10, height - 10));
            p3.CreateSegments();
            polygons.AddLast(p3);
        }
    }
}
