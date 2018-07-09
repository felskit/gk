using Sketcher.Enums;

namespace Sketcher.Models.Geometry
{
    public interface ISweepLineEvent
    {
        int X { get; set; }
        SweepLineEventType Type { get; set; }
    }
}
