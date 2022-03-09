namespace RayTracer.Lib;

public record Sphere(Point Centre, double Radius, Colour Colour, int? Specular = null, double Reflective = 0) 
    : SphereBase(Centre, Radius)
{
    public Guid Identifier { get; } = Guid.NewGuid();
}
