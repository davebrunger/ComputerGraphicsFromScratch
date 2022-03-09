namespace Rasterizer.Lib;

public static class CanvasExtensions
{
    public static void Initialize(this ICanvas canvas, Colour colour)
    {
        foreach (var x in Enumerable.Range(-canvas.Width / 2, canvas.Width))
        {
            foreach (var y in  Enumerable.Range(-canvas.Height / 2, canvas.Height))
            {
                canvas.PutPixel(x, y, colour);
            }
        }
    }

    public static void DrawLine(this ICanvas canvas, Line line, Colour colour)
    {
        if (Math.Abs(line.P1.X - line.P0.X) > Math.Abs(line.P1.Y - line.P0.Y))
        {
            canvas.DrawHorizontalishLine(line, colour);
        }
        else
        {
            canvas.DrawVerticalishLine(line, colour);
        }
    }

    public static void DrawWireFrameTriangle(this ICanvas canvas, Triangle triangle, Colour colour)
    {
        canvas.DrawLine(new Line(triangle.P0, triangle.P1), colour);
        canvas.DrawLine(new Line(triangle.P1, triangle.P2), colour);
        canvas.DrawLine(new Line(triangle.P2, triangle.P0), colour);
    }

    public static void DrawFilledTriangle(this ICanvas canvas, Triangle triangle, Colour colour)
    {
        var points = new[] { triangle.P0, triangle.P1, triangle.P2 }.OrderBy(p => p.Y).ToArray();
        
        var shortSidesXs = Utilities.Interpolate(points[0].Y, points[0].X, points[1].Y, points[1].X)
            .Concat(Utilities.Interpolate(points[1].Y, points[1].X, points[2].Y, points[2].X).Skip(1))
            .ToArray();
        var longSideXs = Utilities.Interpolate(points[0].Y, points[0].X, points[2].Y, points[2].X)
            .ToArray();
        
        var middle = longSideXs.Length / 2;
        var (leftSideXs, rightSideXs) = longSideXs[middle] < shortSidesXs[middle]
            ? (longSideXs, shortSidesXs)
            : (shortSidesXs, longSideXs);

        for (var y = points[0].Y; y <= points[2].Y; y++)
        {
            var yOffset = y - points[0].Y;
            var maxX = (int)rightSideXs[yOffset];
            for (var x = (int)leftSideXs[yOffset]; x <= maxX; x++)
            {
                canvas.PutPixel(x, y, colour);
            }
        }
    }

    public static void DrawHorizontalishLine(this ICanvas canvas, Line line, Colour colour)
    {
        var (p0, p1) = line.P0.X > line.P1.X
            ? (line.P1, line.P0)
            : (line.P0, line.P1);
        foreach (var p in Utilities.Interpolate(p0.X, p0.Y, p1.X, p1.Y).Select((y, xOffset) => (y, xOffset)))
        {
            canvas.PutPixel(p0.X + p.xOffset, (int)p.y, colour);
        }
    }

    private static void DrawVerticalishLine(this ICanvas canvas, Line line, Colour colour)
    {
        var (p0, p1) = line.P0.Y > line.P1.Y
            ? (line.P1, line.P0)
            : (line.P0, line.P1);
        foreach (var p in Utilities.Interpolate(p0.Y, p0.X, p1.Y, p1.X).Select((x, yOffset) => (x, yOffset)))
        {
            canvas.PutPixel((int)p.x, p0.Y + p.yOffset, colour);
        }
    }
}
