namespace Graphics.Common.ThreeDimensions;

public interface ITransform
{
    double Scale { get; }
    double Rotation { get; }
    Point Translation { get; }
}

