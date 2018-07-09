using System;

namespace ColorExtractor.Helpers.Strategies
{
    public class RgbStrategy : IStrategy
    {
        public void ProcessPixel(int i, int j, double r, double g, double b, DirectBitmap[] channels)
        {
            channels[0].SetPixel(i, j, 255 << 24 | (int)Math.Round(r * 255) << 16);
            channels[1].SetPixel(i, j, 255 << 24 | (int)Math.Round(g * 255) << 8);
            channels[2].SetPixel(i, j, 255 << 24 | (int)Math.Round(b * 255));
        }
    }
}
