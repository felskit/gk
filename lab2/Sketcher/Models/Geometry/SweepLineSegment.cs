namespace Sketcher.Models.Geometry
{
    public class SweepLineSegment
    {
        public Segment Segment { get; set; }
        public int Xmin { get; set; }
        public double Y { get; set; }
        public double Gradient { get; set; }

        public SweepLineSegment(Segment segment, int xmin, double y, double gradient)
        {
            Segment = segment;
            Xmin = xmin;
            Y = y;
            Gradient = gradient;
        }
    }
}
