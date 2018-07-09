using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Sketcher.Models
{
    public class Polygon
    {
        private readonly List<Point> _previousPositions = new List<Point>();

        public LinkedList<Vertex> Vertices { get; } = new LinkedList<Vertex>();
        public LinkedList<Segment> Segments { get; } = new LinkedList<Segment>();

        public Vertex FirstVertex => Vertices.FirstOrDefault();
        public Vertex LastVertex => Vertices.LastOrDefault();

        public bool IsGrabbed(Vertex clicked)
        {
            var ray = new Segment(clicked, new Vertex(clicked.X + 1920, clicked.Y));
            var intersections = Segments.Count(s => ray.Intersects(s));
            var vertexHits = Vertices.Count(v => v.Y == clicked.Y && v.X >= clicked.X);
            return (intersections - vertexHits) % 2 == 1;
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

            leftSegment.Constraint = null;
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
            s.Constraint = null;
            s.To = middle;
        }

        public bool TryApplyConstraints(Vertex v)
        {
            var segment = Segments.FirstOrDefault(s => s.From == v);
            if (segment == null) return false;

            var fw = Segments.Find(segment);
            if (fw == null) return false;
            var bw = fw.Previous ?? Segments.Last;

            while (true)
            {
                fw.Value.Constraint?.Apply(true);
                bw.Value.Constraint?.Apply(false);

                if (fw.Value == bw.Value) break;
                fw = fw.Next ?? Segments.First;
                if (fw.Value == bw.Value) break;
                bw = bw.Previous ?? Segments.Last;
            }

            return Segments.All(s => s.Constraint?.Validate() != false);
        }

        public void PreserveVertices()
        {
            _previousPositions.Clear();
            foreach (var vertex in Vertices)
            {
                _previousPositions.Add(new Point(vertex.X, vertex.Y));
            }
        }

        public void RestoreVertices()
        {
            if (Vertices.Count != _previousPositions.Count) return;

            int i = 0;
            foreach (var vertex in Vertices)
            {
                vertex.X = _previousPositions[i].X;
                vertex.Y = _previousPositions[i].Y;
                i++;
            }
        }
    }
}
