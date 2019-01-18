using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VRGraph.Utilities
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float Length()
        {
            return (float)Math.Sqrt(LengthSquared());
        }
        public float LengthSquared()
        {
            return X * X + Y * Y + Z * Z;
        }
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return Zip(a, b, (n, m) => n + m);
        }
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return Zip(a, b, (n, m) => n - m);
        }
        public static float operator *(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
        public static Vector3 operator *(Vector3 v, float f)
        {
            return Select(v, x => x * f);
        }
        public static Vector3 operator *(float f, Vector3 v)
        {
            return v * f;
        }
        public static Vector3 operator /(Vector3 v, float f)
        {
            return Select(v, x => x / f);
        }
        private static Vector3 Zip(Vector3 a, Vector3 b, Func<float, float, float> func)
        {
            return new Vector3(func(a.X, b.X), func(a.Y, b.Y), func(a.Z, b.Z));
        }
        private static Vector3 Select(Vector3 a, Func<float, float> func)
        {
            return new Vector3(func(a.X), func(a.Y), func(a.Z));
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Vector3))
                return false;
            Vector3 other = (Vector3)obj;
            return X == other.X && Y == other.Y && Z == other.Z;
        }
        public static bool operator ==(Vector3 c1, Vector3 c2)
        {
            return c1.Equals(c2);
        }
        public static bool operator !=(Vector3 c1, Vector3 c2)
        {
            return !c1.Equals(c2);
        }
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        }
        public override string ToString()
        {
            return $"<{X}, {Y}, {Z}>";
        }
    }
}