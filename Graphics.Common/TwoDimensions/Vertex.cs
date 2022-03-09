namespace Graphics.Common.TwoDimensions;

public record Vertex(int X, int Y, double H = 1.0) : Point(X, Y);