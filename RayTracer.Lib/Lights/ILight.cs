namespace RayTracer.Lib.Lights;

public interface ILight
{
    double Intensity { get; }
    double GetIntensityAt(Point point, Vector normal, Vector viewer, int? specularPower, IEnumerable<Sphere> spheres);
}
