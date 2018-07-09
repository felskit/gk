using System;
using System.Linq;

namespace ColorExtractor.Helpers.Strategies
{
    public class HsvStrategy : IStrategy
    {
        private const double Tolerance = 1e-6;

        public void ProcessPixel(int i, int j, double r, double g, double b, DirectBitmap[] channels)
        {
            var cmax = new[] { r, g, b }.Max();
            var cmin = new[] { r, g, b }.Min();
            var delta = cmax - cmin;

            var val = Math.Round(cmax * 255);
            var sat = Math.Abs(cmax) < Tolerance ? 0 : Math.Round(delta / cmax * 255);

            double hue;
            if (Math.Abs(delta) < Tolerance)
            {
                hue = 0;
            }
            else
            {
                if (Math.Abs(cmax - r) < Tolerance)
                {
                    hue = (g - b) / delta;
                }
                else if (Math.Abs(cmax - g) < Tolerance)
                {
                    hue = (b - r) / delta + 2;
                }
                else
                {
                    hue = (r - g) / delta + 4;
                }

                hue = Math.Round(General.Mod(hue / 6 * 255, 255));
            }

            channels[0].SetPixel(i, j, General.ToGrayscaleArgb((int)hue));
            channels[1].SetPixel(i, j, General.ToGrayscaleArgb((int)sat));
            channels[2].SetPixel(i, j, General.ToGrayscaleArgb((int)val));
        }
    }
}
