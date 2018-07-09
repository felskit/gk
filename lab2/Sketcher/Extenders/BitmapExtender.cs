using System.Drawing;

namespace Sketcher.Extenders
{
    public static class BitmapExtender
    {
        public static void Clear(this Bitmap bitmap, Brush brush)
        {
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
            }
        }
    }
}
