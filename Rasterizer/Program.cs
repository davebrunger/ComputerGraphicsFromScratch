var viewPort = new ViewPort(1.78 * 1, 1 * 1, 1);
var canvas = new BitmapCanvas(1920, 1080);

//var line1 = new Line(new Point(-200, -100), new Point(240, 120));
//var line2 = new Line(new Point(-50, -200), new Point(60, 240));
//var triangle = new Triangle(new Vertex(-200, -250, 0), new Vertex(200, 50, 0), new Vertex(20, 250));

canvas.Initialize(Colours.White);
//canvas.DrawLine(line1, Colours.Red);
//canvas.DrawLine(line2, Colours.Red);
//canvas.DrawFilledTriangle(triangle, Colours.Green);
//canvas.DrawWireFrameTriangle(triangle, Colours.Black);
//canvas.DrawShadedTriangle(triangle, Colours.Green);

// The four "front" vertices

var cube = new Model(
    new[]
    {
        new Point3(1, 1, 1),
        new Point3(-1, 1, 1),
        new Point3(-1, -1, 1),
        new Point3(1, -1, 1),
        new Point3(1, 1, -1),
        new Point3(-1, 1, -1),
        new Point3(-1, -1, -1),
        new Point3(1, -1, -1),
    }.ToImmutableList(),
    new[]
    {
        new Model.Triangle(0, 1, 2, Colours.Red),
        new Model.Triangle(0, 2, 3, Colours.Red),
        new Model.Triangle(4, 0, 3, Colours.Green),
        new Model.Triangle(4, 3, 7, Colours.Green),
        new Model.Triangle(5, 4, 7, Colours.Blue),
        new Model.Triangle(5, 7, 6, Colours.Blue),
        new Model.Triangle(1, 5, 6, Colours.Yellow),
        new Model.Triangle(1, 6, 2, Colours.Yellow),
        new Model.Triangle(4, 5, 1, Colours.Purple),
        new Model.Triangle(4, 1, 0, Colours.Purple),
        new Model.Triangle(2, 6, 7, Colours.Cyan),
        new Model.Triangle(2, 7, 3, Colours.Cyan),
    }.ToImmutableList());

var objects = new[]
{
    new Object3(cube, 1, 0, new Point3(0, 0, 5)),
    new Object3(cube, 1, 0, new Point3(1, 2, 3))
};

Renderer.RenderScene(objects, viewPort, canvas);

await canvas.SaveAsync($"C:\\Temp\\{DateTime.Now:yyyyMMddHHmmss}.bmp", CancellationToken.None);
Console.WriteLine("Done");
