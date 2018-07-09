using System;

namespace ColorExtractor.Models
{
    public class Matrix3
    {
        private readonly double[,] _values;

        public double this[int i, int j]
        {
            get { return _values[i, j]; }
            set { _values[i, j] = value; }
        }

        public Matrix3()
        {
            _values = new double[3, 3];
        }

        public Matrix3(Vector3 diag)
        {
            _values = new double[3, 3];
            for (int i = 0; i < 3; i++)
            {
                _values[i, i] = diag[i];
            }
        }

        public Matrix3(Vector3 c1, Vector3 c2, Vector3 c3)
        {
            _values = new double[3, 3];
            SetColumn(0, c1);
            SetColumn(1, c2);
            SetColumn(2, c3);
        }

        public Matrix3 Inverse()
        {
            var det = _values[0, 0] * (_values[1, 1] * _values[2, 2] - _values[2, 1] * _values[1, 2]) -
                      _values[0, 1] * (_values[1, 0] * _values[2, 2] - _values[1, 2] * _values[2, 0]) +
                      _values[0, 2] * (_values[1, 0] * _values[2, 1] - _values[1, 1] * _values[2, 0]);

            if (Math.Abs(det) < 1e-12) return null;
            var invdet = 1 / det;

            return new Matrix3
            {
                [0, 0] = (_values[1, 1] * _values[2, 2] - _values[2, 1] * _values[1, 2]) * invdet,
                [0, 1] = (_values[0, 2] * _values[2, 1] - _values[0, 1] * _values[2, 2]) * invdet,
                [0, 2] = (_values[0, 1] * _values[1, 2] - _values[0, 2] * _values[1, 1]) * invdet,
                [1, 0] = (_values[1, 2] * _values[2, 0] - _values[1, 0] * _values[2, 2]) * invdet,
                [1, 1] = (_values[0, 0] * _values[2, 2] - _values[0, 2] * _values[2, 0]) * invdet,
                [1, 2] = (_values[1, 0] * _values[0, 2] - _values[0, 0] * _values[1, 2]) * invdet,
                [2, 0] = (_values[1, 0] * _values[2, 1] - _values[2, 0] * _values[1, 1]) * invdet,
                [2, 1] = (_values[2, 0] * _values[0, 1] - _values[0, 0] * _values[2, 1]) * invdet,
                [2, 2] = (_values[0, 0] * _values[1, 1] - _values[1, 0] * _values[0, 1]) * invdet
            };
        }

        public static Vector3 operator *(Matrix3 m, Vector3 v)
        {
            var retVal = new Vector3();
            for (int i = 0; i < 3; i++)
            {
                retVal[i] = 0.0;
                for (int j = 0; j < 3; j++)
                {
                    retVal[i] += m[i, j] * v[j];
                }
            }

            return retVal;
        }

        public static Matrix3 operator *(Matrix3 m1, Matrix3 m2)
        {
            var retVal = new Matrix3();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    retVal[i, j] = 0.0;
                    for (int k = 0; k < 3; k++)
                    {
                        retVal[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }

            return retVal;
        }

        private void SetColumn(int c, Vector3 v)
        {
            for (int r = 0; r < 3; r++)
            {
                _values[r, c] = v[r];
            }
        }
    }
}