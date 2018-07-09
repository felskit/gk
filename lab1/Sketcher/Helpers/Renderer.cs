using System;
using System.Drawing;
using Sketcher.Models;
using Sketcher.Models.Constraints;

namespace Sketcher.Helpers
{
    public static class Renderer
    {
        public static void RenderPolygons(Sketcher sketcher)
        {
            using (var graphics = Graphics.FromImage(sketcher.Background))
            {
                DrawBackground(graphics, sketcher.Background.Width, sketcher.Background.Height);

                foreach (var poly in sketcher.Polygons)
                {
                    foreach (var segment in poly.Segments)
                    {
                        if (segment.Antialiased)
                        {
                            RenderSegmentXiaolinWu(sketcher.Background, segment.From.X, segment.From.Y, segment.To.X, segment.To.Y);
                        }
                        else
                        {
                            RenderSegmentBresenham(sketcher.Background, segment.From.X, segment.From.Y, segment.To.X, segment.To.Y);
                        }

                        if (segment.Constraint == null) continue;
                        RenderConstraint(sketcher.Background, graphics, segment);
                    }

                    foreach (var vertex in poly.Vertices)
                    {
                        RenderVertex(graphics, vertex.X, vertex.Y);
                    }
                }
            }
        }

        private static void DrawBackground(Graphics graphics, int width, int height)
        {
            graphics.FillRectangle(Brushes.White, 0, 0, width, height);
        }

        private static void RenderVertex(Graphics graphics, int x, int y)
        {
            graphics.FillRectangle(Brushes.Black, x - Vertex.Size / 2, y - Vertex.Size / 2, Vertex.Size, Vertex.Size);
        }

        private static void RenderSegmentBresenham(Bitmap bitmap, int x0, int y0, int x1, int y1)
        {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }

            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            var dX = x1 - x0;
            var dY = Math.Abs(y1 - y0);
            var ystep = y0 < y1 ? 1 : -1;
            var err = dX / 2;
            var y = y0;

            for (int x = x0; x <= x1; ++x)
            {
                if (steep)
                {
                    SetPixel(bitmap, y, x, Color.Black);
                }
                else
                {
                    SetPixel(bitmap, x, y, Color.Black);
                }

                err = err - dY;

                if (err < 0)
                {
                    y += ystep;
                    err += dX;
                }
            }
        }

        private static void RenderSegmentXiaolinWu(Bitmap bitmap, int x0, int y0, int x1, int y1)
        {
            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

            if (steep)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }

            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            var m = (double)(y1 - y0) / (x1 - x0);
            var y = (double)y0;

            for (int x = x0; x < x1; x++)
            {
                var inty = (int)y;
                var frac = y - (int)y;
                var intensity = 255 - (int)(frac * 255);

                var color1 = Color.FromArgb(255 - intensity, 255 - intensity, 255 - intensity);
                var color2 = Color.FromArgb(intensity, intensity, intensity);
                
                if (steep)
                {
                    SetPixel(bitmap, inty, x, color1);
                    SetPixel(bitmap, inty + 1, x, color2);
                }
                else
                {
                    SetPixel(bitmap, x, inty, color1);
                    SetPixel(bitmap, x, inty + 1, color2);
                }

                y += m;
            }
        }

        private static void Swap<T>(ref T o1, ref T o2)
        {
            T tmp = o1;
            o1 = o2;
            o2 = tmp;
        }

        private static void RenderConstraint(Bitmap bitmap, Graphics graphics, Segment segment)
        {
            var middle = Vertex.Average(segment.From, segment.To);
            var constraint = segment.Constraint as LengthConstraint;
            if (constraint != null)
            {
                var format = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                };

                var rect = new Rectangle(middle.X - 15, middle.Y - 8, 31, 17);
                graphics.FillRectangle(Brushes.White, rect);
                graphics.DrawRectangle(Pens.Red, rect);

                rect.X++;
                rect.Y++;

                graphics.DrawString(constraint.Length.ToString(), new Font("Consolas", 10), Brushes.Black, rect, format);
            }
            else
            {
                var rect = new Rectangle(middle.X - 9, middle.Y - 9, 18, 18);
                graphics.FillEllipse(Brushes.White, rect);
                graphics.DrawEllipse(Pens.Red, rect);

                DrawLine(bitmap, middle.X, middle.Y, segment.Constraint is VerticalConstraint);
            }
        }

        private static void DrawLine(Bitmap bitmap, int x, int y, bool vertical)
        {
            var xdist = 4;
            var ydist = 0;
            var xstep = 1;
            var ystep = 0;

            if (vertical)
            {
                Swap(ref xdist, ref ydist);
                Swap(ref xstep, ref ystep);
            }

            for (int i = x - xdist, j = y - ydist; i <= x + xdist && j <= y + ydist; i += xstep, j += ystep)
            {
                SetPixel(bitmap, i, j, Color.Black);
            }
        }

        private static void SetPixel(Bitmap bitmap, int i, int j, Color color)
        {
            if (IsWithinBitmap(bitmap, i, j))
            {
                bitmap.SetPixel(i, j, color);
            }
        }

        private static bool IsWithinBitmap(Bitmap bitmap, int x, int y)
        {
            return x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height;
        }
    }
}
