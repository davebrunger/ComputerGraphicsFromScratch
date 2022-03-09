namespace Rasterizer.Lib;

public static class Renderer
{
    public static Point2 ViewPortToCanvas(Point3 viewPortPoint, ViewPort viewPort, ICanvas canvas)
    {
        return new Point2(
            (int)(viewPortPoint.X * canvas.Width / viewPort.Width),
            (int)(viewPortPoint.Y * canvas.Height / viewPort.Height));
    }

    public static Point2 Project(Point3 point, ViewPort viewPort, ICanvas canvas)
    {
        return ViewPortToCanvas(
            new Point3(
                point.X * viewPort.DistanceFromCamera / point.Z,
                point.Y * viewPort.DistanceFromCamera / point.Z,
                viewPort.DistanceFromCamera),
            viewPort, canvas);

    }
}

