using System;

namespace ColorExtractor.Helpers
{
    public static class General
    {
        public static void Swap<T>(ref T o1, ref T o2)
        {
            var tmp = o1;
            o1 = o2;
            o2 = tmp;
        }

        public static double Mod(double v, double m)
        {
            var r = v % m;
            return r < 0 ? r + m : r;
        }

        public static int ToGrayscaleArgb(int value)
        {
            return 255 << 24 | value << 16 | value << 8 | value;
        }

        public static int RgbMean(int color)
        {
            return (int)Math.Round(0.299 * (byte)(color >> 16) + 0.587 * (byte)(color >> 8) + 0.114 * (byte)color);
        }
    }
}
