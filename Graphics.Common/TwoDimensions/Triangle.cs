namespace Graphics.Common.TwoDimensions;

public record Triangle(Vertex P0, Vertex P1, Vertex P2)
{
    public Triangle(Point P0, Point P1, Point P2) : this(new Vertex(P0), new Vertex(P1), new Vertex(P2)) { }
}