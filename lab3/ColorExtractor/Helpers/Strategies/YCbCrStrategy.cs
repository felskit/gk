using System;

namespace ColorExtractor.Helpers.Strategies
{
    public class YCbCrStrategy : IStrategy
    {
        public void ProcessPixel(int i, int j, double r, double g, double b, DirectBitmap[] channels)
        {
            var y = 0.299 * r + 0.587 * g + 0.114 * b;
            var cb = (int)Math.Round(((b - y) / 1.772 + 0.5) * 255);
            var cr = (int)Math.Round(((r - y) / 1.402 + 0.5) * 255);

            //var y = 16 + (int)Math.Round(65.481 * r + 128.553 * g + 24.966 * b);
            //var cb = 128 + (int)Math.Round(-37.797 * r - 74.203 * g + 112.0 * b);
            //var cr = 128 + (int)Math.Round(112.0 * r - 93.786 * g - 18.214 * b);

            channels[0].SetPixel(i, j, General.ToGrayscaleArgb((int)Math.Round(y * 255)));
            channels[1].SetPixel(i, j, 255 << 24 | 128 << 16 | (255 - cb) << 8 | cb);
            channels[2].SetPixel(i, j, 255 << 24 | cr << 16 | (255 - cr) << 8 | 128);
        }
    }
}
