namespace Graphics.Common.ThreeDimensions;

public abstract record SphereBase(Point Centre, double Radius)
{
    public Vector GetNormalAt(Point point) => point - Centre;
}
