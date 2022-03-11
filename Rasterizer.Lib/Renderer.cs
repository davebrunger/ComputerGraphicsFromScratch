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

    public static void RenderObject(Object3 obj, ViewPort viewPort, ICanvas canvas)
    {
        var projected = obj.Model.Vertices.Select(localPoint =>
        {
            var globalPoint = localPoint.Transform(obj);
            return Project(globalPoint, viewPort, canvas);

        }).ToArray();
        foreach (var t in obj.Model.Triangles)
        {
            var canvasTriangle = new Triangle2(projected[t.P0Index], projected[t.P1Index], projected[t.P2Index]);
            canvas.DrawWireFrameTriangle(canvasTriangle, t.Colour);
        }
    }

    public static void RenderScene(IEnumerable<Object3> objects, ViewPort viewPort, ICanvas canvas)
    {
        foreach (var obj in objects)
        {
            RenderObject(obj, viewPort, canvas);
        }
    }
}

