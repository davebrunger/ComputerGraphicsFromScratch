namespace RayTracer.Lib.Lights;

public record DirectionalLight(double Intensity, Vector Direction) : IDirectionalLight
{
    public double TMax => Ray.Infinity;
    public Vector GetDirectionTo(Point point) => Direction;
}

