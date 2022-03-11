namespace Graphics.Common.ThreeDimensions;

public record Model(ImmutableList<Point> Vertices, ImmutableList<Model.Triangle> Triangles)
{
    public record Triangle(int P0Index, int P1Index, int P2Index, Colour Colour);
}

