using ColorExtractor.Exceptions;
using ColorExtractor.Models;
using System;

namespace ColorExtractor.Helpers.Strategies
{
    public class LabStrategy : IStrategy
    {
        private const double Epsilon = 0.008856;
        private const double Kappa = 903.3;

        private readonly double _gamma;
        private readonly Vector3 _referenceWhite;
        private readonly Matrix3 _transformationMatrix;

        // Pomocny okazał się poniższy artykuł:
        // http://www.ryanjuckett.com/programming/rgb-color-space-conversion/

        public LabStrategy(ColorProfile cp)
        {
            var rxyz = new Vector3(cp.Rx, cp.Ry, 1 - cp.Rx - cp.Ry);
            var gxyz = new Vector3(cp.Gx, cp.Gy, 1 - cp.Gx - cp.Gy);
            var bxyz = new Vector3(cp.Bx, cp.By, 1 - cp.Bx - cp.By);
            var wxyz = new Vector3(cp.Wx, cp.Wy, 1 - cp.Wx - cp.Wy) * (1 / cp.Wy);

            var rgbMatrix = new Matrix3(rxyz, gxyz, bxyz);
            var rgbMatrixInverse = rgbMatrix.Inverse();

            if (rgbMatrixInverse == null)
            {
                throw new InvalidColorProfileException();
            }

            var srgb = rgbMatrixInverse * wxyz;
            _transformationMatrix = rgbMatrix * new Matrix3(srgb);
            _referenceWhite = wxyz * 100;
            _gamma = cp.Gamma;
        }

        public void ProcessPixel(int i, int j, double r, double g, double b, DirectBitmap[] channels)
        {
            r = GammaCorrection(r, _gamma);
            g = GammaCorrection(g, _gamma);
            b = GammaCorrection(b, _gamma);

            var xyz = _transformationMatrix * new Vector3(r, g, b);
            var x = PivotXyz(xyz[0] / _referenceWhite[0]);
            var y = PivotXyz(xyz[1] / _referenceWhite[1]);
            var z = PivotXyz(xyz[2] / _referenceWhite[2]);

            var cieL = (int)Math.Round(Math.Max(0, 116 * y - 16) / 100 * 255);
            var ciea = (int)Math.Round(500 * (x - y) + 128);
            var cieb = (int)Math.Round(200 * (y - z) + 128);

            channels[0].SetPixel(i, j, General.ToGrayscaleArgb(cieL));
            channels[1].SetPixel(i, j, 255 << 24 | ciea << 16 | (255 - ciea) << 8 | 128);
            channels[2].SetPixel(i, j, 255 << 24 | cieb << 16 | 128 << 8 | (255 - cieb));
        }

        private static double GammaCorrection(double v, double gamma)
        {
            return Math.Pow(v, gamma) * 100;
        }

        private static double PivotXyz(double v)
        {
            return v > Epsilon ? CubicRoot(v) : (Kappa * v + 16) / 116;
        }

        private static double CubicRoot(double v)
        {
            return Math.Pow(v, 1.0 / 3.0);
        }
    }
}
