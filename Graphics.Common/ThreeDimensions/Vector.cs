namespace Graphics.Common.ThreeDimensions;

public record Vector(double X, double Y, double Z)
{
    public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);
    public double Dot(Vector other) => X * other.X + Y * other.Y + Z * other.Z;
    public Vector Scale(double scale) => new(X * scale, Y * scale, Z * scale);
    public Vector UnitVector => Length == 0 ? this : Scale(1 / Length);
    public Vector Reflect(Vector normal) => 2 * normal * normal.Dot(this) - this;

    public static Vector operator *(Vector vector, double scale) => vector.Scale(scale);
    public static Vector operator *(double scale, Vector vector) => vector.Scale(scale);
    public static Vector operator -(Vector vector) => vector.Scale(-1);
    public static Vector operator -(Vector v1, Vector v2) => new(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
}

