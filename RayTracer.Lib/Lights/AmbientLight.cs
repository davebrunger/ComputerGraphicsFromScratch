namespace RayTracer.Lib.Lights;

public record class AmbientLight(double Intensity) : ILight
{
    public double GetIntensityAt(Point point, Vector normal, Vector viewer, int? specularPower, IEnumerable<Sphere> spheres) => Intensity;
}

