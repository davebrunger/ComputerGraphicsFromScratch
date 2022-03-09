namespace Graphics.Common.ThreeDimensions;

public record Ray(Point Origin, Vector Direction)
{
    public const double Infinity = double.MaxValue;
    public const double Epsilon = 0.0001;

    public (double, double)? GetIntersections(SphereBase sphere)
    {
        var direction = Origin - sphere.Centre;
        var a = Direction.Dot(Direction);
        var b = 2 * direction.Dot(Direction);
        var c = direction.Dot(direction) - sphere.Radius * sphere.Radius;
        var discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            return null;
        }
        var t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
        var t2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
        return (t1, t2);
    }

    public (double T, T Sphere)? GetClosestIntersection<T>(IEnumerable<T> spheres, double tMin, double tMax)
        where T : SphereBase
    {
        (double T, T Sphere)? closest = null;
        foreach (var sphere in spheres)
        {
            var intersections = GetIntersections(sphere);
            if (!intersections.HasValue)
            {
                continue;
            }
            var (t1, t2) = intersections.Value;
            if (t1 >= tMin && t1 <= tMax && (!closest.HasValue || t1 < closest.Value.T))
            {
                closest = (t1, sphere);
            }
            if (t2 >= tMin && t2 <= tMax && (!closest.HasValue || t2 < closest.Value.T))
            {
                closest = (t2, sphere);
            }
        }
        return closest;
    }
}

