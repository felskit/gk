using Sketcher.Enums;

namespace Sketcher.Models.Geometry
{
    public class SegmentEdgeEvent : ISweepLineEvent
    {
        public Segment Segment { get; set; }
        public int X { get; set; }
        public SweepLineEventType Type { get; set; }

        public SegmentEdgeEvent(Segment segment, int x, SweepLineEventType type)
        {
            Segment = segment;
            X = x;
            Type = type;
        }
    }
}
