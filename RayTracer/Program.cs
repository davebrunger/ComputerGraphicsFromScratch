var viewPort = new ViewPort(1.78, 1, 1);
var canvas = new BitmapCanvas(1920, 1080);
var camera = new Camera(new Point(0, 0, 0));

var spheres = new[] {
    new Sphere(new Point(0, -1, 3), 1, Colours.Red, 500, 0.2),
    new Sphere(new Point(2, 0, 4), 1, Colours.Blue, 500, 0.3),
    new Sphere(new Point(-2, 0, 4), 1, Colours.Green, 10, 0.4),
    new Sphere(new Point(0, -5001, 0), 5000, Colours.Yellow, 1000, 0.5),
}.ToImmutableList();
var lights = new ILight[] {
    new AmbientLight(0.2),
    new PointLight(0.6, new Point(2, 1, 0)),
    new DirectionalLight(0.2, new Vector(1, 4, 4)),
}.ToImmutableList();
var scene = new Scene(spheres, lights, Colours.Black);

Renderer.Render(canvas, viewPort, scene, camera, 3);

await canvas.SaveAsync($"C:\\Temp\\{DateTime.Now:yyyyMMddHHmmss}.bmp", CancellationToken.None);
Console.WriteLine("Done");
