namespace Sketcher.Models.Geometry
{
    public class ActiveEdge
    {
        public int Ymax { get; set; }
        public double Xmin { get; set; }
        public double Gradient { get; set; }

        public ActiveEdge(int ymax, double xmin, double gradient)
        {
            Ymax = ymax;
            Xmin = xmin;
            Gradient = gradient;
        }
    }
}
