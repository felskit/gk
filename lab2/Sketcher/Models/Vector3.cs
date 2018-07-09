using System;
using System.Drawing;

namespace Sketcher.Models
{
    public class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 Normalize()
        {
            var len = Length;
            X = X / len;
            Y = Y / len;
            Z = Z / len;
            return this;
        }

        public int ToArgb()
        {
            return (255 << 24) | ((int)(X * 255) << 16) | ((int)(Y * 255) << 8) | (int)(Z * 255);
        }

        public static Vector3 FromArgb(int argb)
        {
            return new Vector3((byte)(argb >> 16) / 255.0, (byte)(argb >> 8) / 255.0, (byte)argb / 255.0);
        }

        public static Vector3 FromColor(Color color)
        {
            return new Vector3(color.R / 255.0, color.G / 255.0, color.B / 255.0);
        }

        public static Vector3 operator *(Vector3 v1, Vector3 v2)
        {
            v1.X *= v2.X;
            v1.Y *= v2.Y;
            v1.Z *= v2.Z;
            return v1;
        }

        public static Vector3 operator *(Vector3 v, double d)
        {
            v.X *= d;
            v.Y *= d;
            v.Z *= d;
            return v;
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            v1.X += v2.X;
            v1.Y += v2.Y;
            v1.Z += v2.Z;
            return v1;
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            v1.X -= v2.X;
            v1.Y -= v2.Y;
            v1.Z -= v2.Z;
            return v1;
        }

        public static double DotProduct(Vector3 v1, Vector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static double CosineAngle(Vector3 v1, Vector3 v2)
        {
            return DotProduct(v1, v2) / (v1.Length * v2.Length);
        }

        internal Vector3 CropToZero()
        {
            if (X > 1) X = 1;
            if (Y > 1) Y = 1;
            if (Z > 1) Z = 1;
            return this;
        }

        internal Vector3 Copy()
        {
            return new Vector3(X, Y, Z);
        }
    }
}
