using Sketcher.Enums;

namespace Sketcher.Models.Geometry
{
    public class SegmentIntersectionEvent : ISweepLineEvent
    {
        public Segment Segment1 { get; set; }
        public Segment Segment2 { get; set; }
        public int X { get; set; }
        public SweepLineEventType Type { get; set; }

        public SegmentIntersectionEvent(Segment segment1, Segment segment2, int x, SweepLineEventType type)
        {
            Segment1 = segment1;
            Segment2 = segment2;
            X = x;
            Type = type;    
        }
    }
}
