namespace RayTracer.Lib.Lights;

public record PointLight(double Intensity, Point Position) : IDirectionalLight
{
    public double TMax => 1;
    public Vector GetDirectionTo(Point point) => Position - point;
}

