var canvas = new BitmapCanvas(1920, 1080);

//var line1 = new Line(new Point(-200, -100), new Point(240, 120));
//var line2 = new Line(new Point(-50, -200), new Point(60, 240));
var triangle = new Triangle(new Vertex(-200, -250, 0), new Vertex(200, 50, 0), new Vertex(20, 250));

canvas.Initialize(Colours.White);
//canvas.DrawLine(line1, Colours.Red);
//canvas.DrawLine(line2, Colours.Red);
//canvas.DrawFilledTriangle(triangle, Colours.Green);
//canvas.DrawWireFrameTriangle(triangle, Colours.Black);
canvas.DrawShadedTriangle(triangle, Colours.Green);

await canvas.SaveAsync($"C:\\Temp\\{DateTime.Now:yyyyMMddHHmmss}.bmp", CancellationToken.None);
Console.WriteLine("Done");
