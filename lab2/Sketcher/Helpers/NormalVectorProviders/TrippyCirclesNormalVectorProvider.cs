using System;
using Sketcher.Models;

namespace Sketcher.Helpers.NormalVectorProviders
{
    public class TrippyCirclesNormalVectorProvider : INormalVectorProvider
    {
        private readonly Func<int, int, Vector3> _normalVectorFormula;
        public Vector3[,] NormalVectors { get; private set; }

        public TrippyCirclesNormalVectorProvider(int width, int height, DirectBitmap heightmap)
        {
            _normalVectorFormula = (x, y) =>
            {
                var ox = (x - width / 2) / 25.0;
                var oy = (y - height / 2) / 25.0;
                var r = ox * ox + oy * oy;
                return new Vector3(-2 * ox * Math.Cos(r), -2 * oy * Math.Cos(r), 1);
            };
            CalculateNormalVectors(width, height, heightmap);
        }

        public void CalculateNormalVectors(int width, int height, DirectBitmap heightmap)
        {
            NormalVectors = new Vector3[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var normalVector = _normalVectorFormula(i, j);
                    NormalVectors[i, j] = heightmap == null ? normalVector.Normalize()
                        : NormalMapper.NormalVectorDistortion(i, j, normalVector, heightmap);
                }
            }
        }
    }
}
