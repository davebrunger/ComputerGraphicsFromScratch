namespace RayTracer.Lib.Lights;

public interface IDirectionalLight : ILight
{
    double TMax { get; }
    Vector GetDirectionTo(Point point);

    double ILight.GetIntensityAt(Point point, Vector normal, Vector viewer, int? specularPower, IEnumerable<Sphere> spheres)
    {
        var direction = GetDirectionTo(point);
        if (new Ray(point, direction).GetClosestIntersection(spheres, Ray.Epsilon, TMax).HasValue)
        {
            return 0;
        }
        return Intensity * (
            GetDiffuseIntensity(direction, normal) +
            GetSpecularIntensity(direction, normal, viewer, specularPower));
    }

    private static double GetDiffuseIntensity(Vector direction, Vector normal)
    {
        var nDotD = normal.Dot(direction);
        if (nDotD <= 0)
        {
            return 0;
        }
        return nDotD / (normal.Length * direction.Length);
    }

    private static double GetSpecularIntensity(Vector direction, Vector normal, Vector viewer, int? specularPower)
    {
        if (!specularPower.HasValue)
        {
            return 0;
        }
        var reflection = direction.Reflect(normal);
        var rDotV = reflection.Dot(viewer);
        if (rDotV <= 0)
        {
            return 0;
        }
        return Math.Pow(rDotV / (reflection.Length * viewer.Length), specularPower.Value);
    }
}

