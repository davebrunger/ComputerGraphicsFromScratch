namespace Graphics.Common.ThreeDimensions;

public record Object(Model Model, double Scale, double Rotation, Point Translation) : ITransform;

