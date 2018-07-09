using System.Collections.Generic;
using System.Linq;
using Sketcher.Models;

namespace Sketcher.Helpers
{
    public static class Geometry
    {
        public static Polygon Union(Polygon p1, Polygon p2)
        {
            if (p1.SignedAreaTimesTwo < 0) p1.Reverse();
            if (p2.SignedAreaTimesTwo < 0) p2.Reverse();

            var segments1 = p1.Segments.ToList();
            var segments2 = p2.Segments.ToList();

            if (NaiveSelfIntersection(segments1) ||
                NaiveSelfIntersection(segments2)) return null;

            RemoveCollinearSegments(segments1, segments2);

            Dictionary<Segment, List<Vertex>> segmentIntersections;
            var intersections = NaiveIntersections(segments1, segments2, out segmentIntersections);
            if (intersections.Count == 0)
            {
                if (p1.Contains(p2.FirstVertex)) return p1;
                if (p2.Contains(p1.FirstVertex)) return p2;
                return null;
            }

            foreach (var s in segmentIntersections.Keys)
            {
                var end = p1.Vertices.Find(s.To);
                var vertices = end != null ? p1.Vertices : p2.Vertices;
                end = end ?? p2.Vertices.Find(s.To);
                if (end == null) return null;

                segmentIntersections[s].Sort((v1, v2) => Vertex.DistSquared(s.From, v1) - Vertex.DistSquared(s.From, v2));
                segmentIntersections[s].ForEach(v => vertices.AddBefore(end, v));
            }

            var leftmost = p1.Vertices.First.Value;
            foreach (var vertex in p1.Vertices.Concat(p2.Vertices).ToList())
            {
                if (vertex.X < leftmost.X)
                {
                    leftmost = vertex;
                }
            }

            var idx = 0;
            var current = p1.Vertices.Find(leftmost);
            if (current == null)
            {
                idx = 1;
                current = p2.Vertices.Find(leftmost);
            }

            var union = new Polygon();
            var extendedVertices = new[] { p1.Vertices, p2.Vertices };

            do {
                if (current == null) return null;
                union.Vertices.AddLast(current.Value);
                current = current.Next ?? extendedVertices[idx].First;

                if (intersections.Contains(current.Value))
                {
                    idx = (idx + 1) % 2;
                    current = extendedVertices[idx].Find(current.Value);
                    if (current == null) return null;
                }
            } while (current.Value != leftmost);

            union.CreateSegments();
            return union;
        }

        private static void RemoveCollinearSegments(List<Segment> segments1, List<Segment> segments2)
        {
            foreach (var s1 in segments1)
            {
                foreach (var s2 in segments2)
                {
                    var dirFrom = (s1.To - s1.From) * (s2.From - s1.From);
                    var dirTo = (s1.To - s1.From) * (s2.To - s1.From);

                    if (dirFrom == 0 && dirTo == 0)
                    {
                        s1.From.X++; s1.To.X--;
                        s1.From.Y++; s1.To.Y--;
                        s2.From.X--; s2.To.X++;
                        s2.From.Y--; s2.To.Y++;
                    }
                }
            }
        }

        public static List<Vertex> NaiveIntersections(List<Segment> segments1, List<Segment> segments2, out Dictionary<Segment, List<Vertex>> segmentIntersections)
        {
            var intersections = new List<Vertex>();
            segmentIntersections = new Dictionary<Segment, List<Vertex>>();

            foreach (var s1 in segments1)
            {
                foreach (var s2 in segments2)
                {
                    if (s1.Intersects(s2))
                    {
                        if (!segmentIntersections.ContainsKey(s1))
                        {
                            segmentIntersections.Add(s1, new List<Vertex>());
                        }

                        if (!segmentIntersections.ContainsKey(s2))
                        {
                            segmentIntersections.Add(s2, new List<Vertex>());
                        }

                        var v = s1.Intersection(s2);
                        segmentIntersections[s1].Add(v);
                        segmentIntersections[s2].Add(v);
                        intersections.Add(v);
                    }
                }
            }

            return intersections;
        }

        private static bool NaiveSelfIntersection(List<Segment> segments)
        {
            return segments.Any(segment => segments.Any(segment.Intersects));
        }

        #region SweepLine
        //public static Dictionary<Segment, List<Vertex>> SweepLineIntersections(List<Segment> segments, out Dictionary<Vertex, bool> intersections)
        //{
        //    intersections = new Dictionary<Vertex, bool>();
        //    var segmentIntersections = new Dictionary<Segment, List<Vertex>>();
        //    if (segments.Count < 2) return segmentIntersections;

        //    var sweepLineEvents = new List<ISweepLineEvent>();
        //    foreach (var segment in segments)
        //    {
        //        var xmin = Math.Min(segment.From.X, segment.To.X);
        //        var xmax = Math.Max(segment.From.X, segment.To.X);
        //        sweepLineEvents.Add(new SegmentEdgeEvent(segment, xmin, SweepLineEventType.Start));
        //        sweepLineEvents.Add(new SegmentEdgeEvent(segment, xmax, SweepLineEventType.End));
        //    }
        //    sweepLineEvents.Sort((e1, e2) => e1.X.CompareTo(e2.X));

        //    var prev = sweepLineEvents[0].X;
        //    var sweepLine = new List<SweepLineSegment>();

        //    while (sweepLineEvents.Count > 0)
        //    {
        //        var e = sweepLineEvents[0];
        //        sweepLineEvents.RemoveAt(0);
        //        sweepLine.ForEach(x => x.Y += x.Gradient * (e.X - prev));
        //        sweepLine.Sort((e1, e2) => e1.Y.CompareTo(e2.Y)); // ???

        //        switch (e.Type)
        //        {
        //            case SweepLineEventType.Start:
        //            {
        //                var start = (SegmentEdgeEvent)e;
        //                var dX = start.Segment.To.X - start.Segment.From.X;
        //                var dY = start.Segment.To.Y - start.Segment.From.Y;
        //                var y = e.X == start.Segment.From.X ? start.Segment.From.Y : start.Segment.To.Y;
        //                sweepLine.Add(new SweepLineSegment(start.Segment, e.X, y, (double)dX / dY));
        //                sweepLine.Sort((e1, e2) => e1.Y.CompareTo(e2.Y));

        //                var idx = sweepLine.FindIndex(x => x.Segment == start.Segment);
        //                if (idx > 0 && sweepLine[idx - 1].Segment.Intersects(start.Segment))
        //                {
        //                    FindIntersection(sweepLine[idx - 1].Segment, start.Segment, sweepLineEvents, segmentIntersections, intersections);
        //                }
        //                if (idx < sweepLine.Count - 1 && sweepLine[idx + 1].Segment.Intersects(start.Segment))
        //                {
        //                    FindIntersection(sweepLine[idx + 1].Segment, start.Segment, sweepLineEvents, segmentIntersections, intersections);
        //                }

        //                break;
        //            }
        //            case SweepLineEventType.End:
        //            {
        //                var idx = sweepLine.FindIndex(x => x.Segment == ((SegmentEdgeEvent)e).Segment);
        //                if (idx > 0 && idx < sweepLine.Count - 1 && sweepLine[idx - 1].Segment.Intersects(sweepLine[idx + 1].Segment))
        //                {
        //                    FindIntersection(sweepLine[idx - 1].Segment, sweepLine[idx + 1].Segment, sweepLineEvents, segmentIntersections, intersections);
        //                }
        //                sweepLine.Remove(sweepLine[idx]);

        //                break;
        //            }
        //            case SweepLineEventType.Intersection:
        //            {
        //                var intersect = (SegmentIntersectionEvent)e;
        //                var idx1 = sweepLine.FindIndex(x => x.Segment == intersect.Segment1);
        //                var idx2 = sweepLine.FindIndex(x => x.Segment == intersect.Segment2);
        //                var idxMin = Math.Min(idx1, idx2);
        //                var idxMax = Math.Max(idx1, idx2);

        //                if (idxMin > 0 && sweepLine[idxMin - 1].Segment.Intersects(intersect.Segment2))
        //                {
        //                    FindIntersection(sweepLine[idxMin - 1].Segment, intersect.Segment2, sweepLineEvents, segmentIntersections, intersections);
        //                }
        //                if (idxMax < sweepLine.Count - 1 && sweepLine[idxMax + 1].Segment.Intersects(intersect.Segment1))
        //                {
        //                    FindIntersection(sweepLine[idxMax + 1].Segment, intersect.Segment1, sweepLineEvents, segmentIntersections, intersections);
        //                }

        //                break;
        //            }
        //        }

        //        prev = e.X;
        //    }

        //    return segmentIntersections;
        //}

        //private static bool SweepLineSelfIntersection(List<Segment> segments)
        //{
        //    if (segments.Count < 2) return false;
        //    var sweepLineEvents = new List<SegmentEdgeEvent>();

        //    foreach (var segment in segments)
        //    {
        //        var xmin = Math.Min(segment.From.X, segment.To.X);
        //        var xmax = Math.Max(segment.From.X, segment.To.X);
        //        sweepLineEvents.Add(new SegmentEdgeEvent(segment, xmin, SweepLineEventType.Start));
        //        sweepLineEvents.Add(new SegmentEdgeEvent(segment, xmax, SweepLineEventType.End));
        //    }
        //    sweepLineEvents.Sort((e1, e2) => e1.X.CompareTo(e2.X));

        //    var prev = sweepLineEvents[0].X;
        //    var sweepLine = new List<SweepLineSegment>();

        //    while (sweepLineEvents.Count > 0)
        //    {
        //        var e = sweepLineEvents[0];
        //        sweepLineEvents.RemoveAt(0);
        //        sweepLine.ForEach(x => x.Y += x.Gradient * (e.X - prev));
        //        sweepLine.Sort((e1, e2) => e1.Y.CompareTo(e2.Y)); // ???

        //        switch (e.Type)
        //        {
        //            case SweepLineEventType.Start:
        //            {
        //                var dX = e.Segment.To.X - e.Segment.From.X;
        //                var dY = e.Segment.To.Y - e.Segment.From.Y;
        //                var y = e.X == e.Segment.From.X ? e.Segment.From.Y : e.Segment.To.Y;
        //                sweepLine.Add(new SweepLineSegment(e.Segment, e.X, y, (double)dX / dY));
        //                sweepLine.Sort((e1, e2) => e1.Y.CompareTo(e2.Y));

        //                var idx = sweepLine.FindIndex(x => x.Segment == e.Segment);
        //                if (idx > 0 && sweepLine[idx - 1].Segment.Intersects(e.Segment))
        //                {
        //                    return true;
        //                }
        //                if (idx < sweepLine.Count - 1 && sweepLine[idx + 1].Segment.Intersects(e.Segment))
        //                {
        //                    return true;
        //                }

        //                break;
        //            }
        //            case SweepLineEventType.End:
        //            {
        //                var idx = sweepLine.FindIndex(x => x.Segment == e.Segment);
        //                if (idx > 0 && idx < sweepLine.Count - 1 && sweepLine[idx - 1].Segment.Intersects(sweepLine[idx + 1].Segment))
        //                {
        //                    return true;
        //                }
        //                sweepLine.Remove(sweepLine[idx]);

        //                break;
        //            }
        //        }

        //        prev = e.X;
        //    }

        //    return false;
        //}

        //private static void FindIntersection(Segment s1, Segment s2, List<ISweepLineEvent> sweepLineEvents, Dictionary<Segment, List<Vertex>> segmentIntersections, Dictionary<Vertex, bool> intersections)
        //{
        //    if (!segmentIntersections.ContainsKey(s1))
        //    {
        //        segmentIntersections.Add(s1, new List<Vertex>());
        //    }

        //    if (!segmentIntersections.ContainsKey(s2))
        //    {
        //        segmentIntersections.Add(s2, new List<Vertex>());
        //    }

        //    var v = s1.Intersection(s2);
        //    segmentIntersections[s1].Add(v);
        //    segmentIntersections[s2].Add(v);

        //    sweepLineEvents.Add(new SegmentIntersectionEvent(s1, s2, v.X, SweepLineEventType.Intersection));
        //    sweepLineEvents.Sort((e1, e2) => e1.X.CompareTo(e2.X));

        //    //intersections.Add(v, v is exit ? true : false);
        //}
        #endregion
    }
}
