namespace Graphics.Common.ThreeDimensions;

public record Point(double X, double Y, double Z)
{
    public Vector Minus(Point other) => new(X - other.X, Y - other.Y, Z - other.Z);
    public static Vector operator -(Point p1, Point p2) => p1.Minus(p2);
    public Point Add(Vector vector) => new(X + vector.X, Y + vector.Y, Z + vector.Z);
    public static Point operator +(Point p, Vector v) => p.Add(v);
}

