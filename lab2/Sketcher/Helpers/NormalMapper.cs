using Sketcher.Models;

namespace Sketcher.Helpers
{
    public static class NormalMapper
    {
        public static Vector3 NormalVectorDistortion(int x, int y, Vector3 normalVector, DirectBitmap directHeightmap)
        {
            var t = normalVector.X / normalVector.Z;
            var b = normalVector.Y / normalVector.Z;

            var xy = (byte)directHeightmap.GetPixel(x, y);
            var hx = t > 0 ? (byte)directHeightmap.GetPixel(x + 1, y) : (byte)directHeightmap.GetPixel(x - 1, y);
            var hy = b > 0 ? (byte)directHeightmap.GetPixel(x, y + 1) : (byte)directHeightmap.GetPixel(x, y - 1);
            var dx = (hx - xy) / 255.0;
            var dy = (hy - xy) / 255.0;

            normalVector.X += dx;
            normalVector.Y += dy;
            normalVector.Z += dx * t + dy * b;

            return normalVector.Normalize();
        }
    }
}
