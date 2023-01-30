using System;

namespace ProjectDiorama
{
    public struct GridPositionXZ
    {

        public int X;
        public int Z;

        public GridPositionXZ(int x, int z)
        {
            X = x;
            Z = z;
        }

        public override string ToString()
        {
            return $"x: {X}; z: {Z}";
        }

        public static bool operator ==(GridPositionXZ a, GridPositionXZ b)
        {
            return a.X == b.X && a.Z == b.Z;
        }

        public static bool operator !=(GridPositionXZ a, GridPositionXZ b)
        {
            return !(a == b);
        }

        public static GridPositionXZ operator +(GridPositionXZ a, GridPositionXZ b)
        {
            return new GridPositionXZ(a.X + b.X, a.Z + b.Z);
        }

        public static GridPositionXZ operator -(GridPositionXZ a, GridPositionXZ b)
        {
            return new GridPositionXZ(a.X - b.X, a.Z - b.Z);
        }

        public bool Equals(GridPositionXZ other)
        {
            return X == other.X && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            return obj is GridPositionXZ other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Z);
        }
    }
}