using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sketcher.Extenders;
using Sketcher.Helpers.LightProviders;
using Sketcher.Helpers.NormalVectorProviders;
using Sketcher.Models;
using Sketcher.Models.Geometry;

namespace Sketcher.Helpers
{
    public static class Renderer
    {
        public static void RenderPolygons(Sketcher sketcher, LinkedList<Polygon> polygons)
        {
            sketcher.Background.Bitmap.Clear(Brushes.White);

            using (var graphics = Graphics.FromImage(sketcher.Background.Bitmap))
            {
                foreach (var polygon in Enumerable.Reverse(polygons))
                {
                    if (polygon.Filled)
                    {
                        FillPolygon(sketcher, sketcher.Background, sketcher.PolygonBackground, sketcher.LightColor,
                            sketcher.LightProvider, sketcher.NormalVectorProvider, polygon);
                    }

                    foreach (var segment in polygon.Segments)
                    {
                        BresenhamLine(sketcher.Background, segment, polygon.OutlineColor);
                    }

                    foreach (var vertex in polygon.Vertices)
                    {
                        DrawVertex(graphics, vertex, polygon.OutlineColor);
                    }
                }
            }
        }

        private static void FillPolygon(Sketcher sketcher, DirectBitmap directBitmap, DirectBitmap directBackground, Color lightColor, ILightProvider lightProvider, INormalVectorProvider normalVectorProvider, Polygon polygon)
        {
            var edgeTable = new Dictionary<int, List<ActiveEdge>>();

            foreach (var segment in polygon.Segments)
            {
                var ymax = Math.Max(segment.From.Y, segment.To.Y);
                var ymin = Math.Min(segment.From.Y, segment.To.Y);
                var xmin = segment.From.Y < segment.To.Y ? segment.From.X : segment.To.X;
                var dX = segment.To.X - segment.From.X;
                var dY = segment.To.Y - segment.From.Y;

                if (!edgeTable.ContainsKey(ymin))
                {
                    edgeTable.Add(ymin, new List<ActiveEdge>());
                }

                edgeTable[ymin].Add(new ActiveEdge(ymax, xmin, (double)dX / dY));
            }

            var y = edgeTable.Keys.Min();
            var activeEdgeTable = new List<ActiveEdge>();

            while (y < directBitmap.Height && (edgeTable.Count > 0 || activeEdgeTable.Count > 0))
            {
                if (edgeTable.ContainsKey(y))
                {
                    activeEdgeTable.AddRange(edgeTable[y]);
                    edgeTable.Remove(y);
                }

                activeEdgeTable.RemoveAll(e => e.Ymax == y);
                activeEdgeTable.Sort((e1, e2) => e1.Xmin.CompareTo(e2.Xmin));

                if (y >= 0)
                {
                    for (int i = 0; i < activeEdgeTable.Count; i += 2)
                    {
                        var x0 = Math.Max((int)activeEdgeTable[i].Xmin + 1, 0);
                        var x1 = Math.Min((int)activeEdgeTable[i + 1].Xmin, directBitmap.Width - 1);

                        while (x0 <= x1)
                        {
                            var polygonColor = directBackground?.GetPixel(x0, y);
                            var polygonColorVector = polygonColor.HasValue ? Vector3.FromArgb(polygonColor.Value) : Vector3.FromColor(polygon.InteriorColor);
                            var lightColorVector = Vector3.FromColor(lightColor);

                            var lightVector = lightProvider.LightVectorFormula(x0, y);
                            var normalVector = normalVectorProvider.NormalVectors[x0, y];

                            var cosineNL = Vector3.DotProduct(normalVector, lightVector.Normalize());
                            if (cosineNL < 0) cosineNL = 0;

                            var observerVector = sketcher.ObserverVectorFormula(x0, y).Normalize();
                            var reflectionVector = normalVector.Copy() * Vector3.DotProduct(normalVector, lightVector) * 2 - lightVector;

                            var cosineVR = Vector3.DotProduct(observerVector, reflectionVector.Normalize());
                            if (cosineVR < 0) cosineVR = 0;
                            else
                            {
                                var power = cosineVR;
                                for (int m = 0; m < sketcher.M - 1; m++)
                                {
                                    cosineVR *= power;
                                }
                            }

                            var color = polygonColorVector * lightColorVector * cosineNL * sketcher.Kd + lightColorVector * cosineVR * sketcher.Ks;
                            directBitmap.SetPixel(x0++, y, color.CropToZero().ToArgb());
                        }
                    }
                }

                activeEdgeTable.ForEach(e => e.Xmin += e.Gradient);
                y++;
            }
        }

        private static void BresenhamLine(DirectBitmap directBitmap, Segment segment, Color color)
        {
            int x0 = segment.From.X, x1 = segment.To.X,
                y0 = segment.From.Y, y1 = segment.To.Y;

            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                General.Swap(ref x0, ref y0);
                General.Swap(ref x1, ref y1);
            }

            if (x0 > x1)
            {
                General.Swap(ref x0, ref x1);
                General.Swap(ref y0, ref y1);
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
                    SetPixel(directBitmap, y, x, color);
                }
                else
                {
                    SetPixel(directBitmap, x, y, color);
                }

                err = err - dY;

                if (err < 0)
                {
                    y += ystep;
                    err += dX;
                }
            }
        }

        private static void DrawVertex(Graphics graphics, Vertex vertex, Color color)
        {
            using (var brush = new SolidBrush(color))
            {
                graphics.FillRectangle(brush, vertex.X - Vertex.Size / 2, vertex.Y - Vertex.Size / 2, Vertex.Size, Vertex.Size);
            }
        }

        private static void SetPixel(DirectBitmap directBitmap, int x, int y, Color color)
        {
            if (x >= 0 && x < directBitmap.Width && y >= 0 && y < directBitmap.Height)
            {
                directBitmap.SetPixel(x, y, ToArgb(color));
            }
        }

        private static int ToArgb(Color color)
        {
            return (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
        }
    }
}
