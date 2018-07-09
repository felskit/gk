using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sketcher.Extenders;

namespace Sketcher.Models
{
    public class Polygon
    {
        public LinkedList<Vertex> Vertices { get; } = new LinkedList<Vertex>();
        public LinkedList<Segment> Segments { get; } = new LinkedList<Segment>();
        public Color OutlineColor { get; set; } = Color.Black;
        public Color InteriorColor { get; set; } = Color.SkyBlue;
        public bool Filled { get; set; }

        public Vertex FirstVertex => Vertices.FirstOrDefault();
        public Vertex LastVertex => Vertices.LastOrDefault();

        public int SignedAreaTimesTwo
        {
            get
            {
                var signedArea = 0;
                var node = Vertices.First;
                while (node != null)
                {
                    signedArea += node.Value * (node.Next ?? Vertices.First).Value;
                    node = node.Next;
                }
                return signedArea;
            }
        }

        public bool Contains(Vertex vertex)
        {
            var ray = new Segment(vertex, new Vertex(vertex.X + 1920, vertex.Y));
            var intersections = Segments.Count(s => ray.Intersects(s));
            var vertexHits = Vertices.Count(v => v.Y == vertex.Y && v.X >= vertex.X);
            return (intersections - vertexHits) % 2 == 1;
        }

        public bool Contains(Polygon polygon)
        {
            return polygon.Vertices.All(Contains);
        }

        public void DeleteVertex(Vertex v)
        {
            if (Vertices.Count <= 3)
            {
                MessageBox.Show(@"Cannot delete more vertices", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var leftSegment = Segments.FirstOrDefault(s => s.To == v);
            var rightSegment = Segments.FirstOrDefault(s => s.From == v);
            if (leftSegment == null || rightSegment == null) return;

            leftSegment.To = rightSegment.To;
            Segments.Remove(rightSegment);
            Vertices.Remove(v);
        }

        public void SplitSegment(Segment s)
        {
            var snode = Segments.Find(s);
            var vnode = Vertices.Find(s.From);
            if (snode == null || vnode == null) return;

            var middle = Vertex.Average(s.From, s.To);
            Vertices.AddAfter(vnode, middle);
            Segments.AddAfter(snode, new Segment(middle, s.To));
            s.To = middle;
        }

        public void Reverse()
        {
            Vertices.Reverse();
            Segments.Reverse();

            foreach (var segment in Segments)
            {
                segment.Reverse();
            }
        }

        public void Select()
        {
            OutlineColor = Color.DarkRed;
            InteriorColor = Color.IndianRed;
        }

        public void Deselect()
        {
            OutlineColor = Color.Black;
            InteriorColor = Color.SkyBlue;
        }

        public void CreateSegments()
        {
            Segments.Clear();
            var node = Vertices.First;
            while (node != null)
            {
                Segments.AddLast(new Segment(node.Value, (node.Next ?? Vertices.First).Value));
                node = node.Next;
            }
        }
    }
}
