var viewPort = new ViewPort(1.78 * 3, 1 * 3, 1);
var canvas = new BitmapCanvas(1920, 1080);

//var line1 = new Line(new Point(-200, -100), new Point(240, 120));
//var line2 = new Line(new Point(-50, -200), new Point(60, 240));
var triangle = new Triangle(new Vertex(-200, -250, 0), new Vertex(200, 50, 0), new Vertex(20, 250));

canvas.Initialize(Colours.White);
//canvas.DrawLine(line1, Colours.Red);
//canvas.DrawLine(line2, Colours.Red);
//canvas.DrawFilledTriangle(triangle, Colours.Green);
//canvas.DrawWireFrameTriangle(triangle, Colours.Black);
//canvas.DrawShadedTriangle(triangle, Colours.Green);

// The four "front" vertices
var vAf = new Point3(-1, 1, 1);
var vBf = new Point3(1, 1, 1);
var vCf = new Point3(1, -1, 1);
var vDf = new Point3(-1, -1, 1);
// The four "back" vertices
var vAb = new Point3(-1, 1, 2);
var vBb = new Point3(1, 1, 2);
var vCb = new Point3(1, -1, 2);
var vDb = new Point3(-1, -1, 2);
// The front face
Point2 project(Point3 point) => Renderer.Project(point, viewPort, canvas);

canvas.DrawLine(new Line(project(vAf), project(vBf)), Colours.Blue);
canvas.DrawLine(new Line(project(vBf), project(vCf)), Colours.Blue);
canvas.DrawLine(new Line(project(vCf), project(vDf)), Colours.Blue);
canvas.DrawLine(new Line(project(vDf), project(vAf)), Colours.Blue);
// The back face                           
canvas.DrawLine(new Line(project(vAb), project(vBb)), Colours.Red);
canvas.DrawLine(new Line(project(vBb), project(vCb)), Colours.Red);
canvas.DrawLine(new Line(project(vCb), project(vDb)), Colours.Red);
canvas.DrawLine(new Line(project(vDb), project(vAb)), Colours.Red);
// The front-to-back edges
canvas.DrawLine(new Line(project(vAf), project(vAb)), Colours.Green);
canvas.DrawLine(new Line(project(vBf), project(vBb)), Colours.Green);
canvas.DrawLine(new Line(project(vCf), project(vCb)), Colours.Green);
canvas.DrawLine(new Line(project(vDf), project(vDb)), Colours.Green);

await canvas.SaveAsync($"C:\\Temp\\{DateTime.Now:yyyyMMddHHmmss}.bmp", CancellationToken.None);
Console.WriteLine("Done");
