namespace RayTracer.Lib;

public class Renderer
{
    public static void Render(ICanvas canvas, ViewPort viewPort, Scene scene, Camera camera, int recursionDepth)
    {
        var firstPass = Enumerable.Range(-canvas.Width / 2, canvas.Width)
            .AsParallel()
            .Select(x => Enumerable.Range(-canvas.Height / 2, canvas.Height)
                .AsParallel()
                .Select(y => (X: x, Y: y, Sample: Sample(canvas, viewPort, scene, camera, recursionDepth, x, y)))
                .ToArray())
            .ToArray();
        for (var x = 0; x < firstPass.Length; x++)
        {
            for (var y = 0; y < firstPass[x].Length; y++)
            {
                var sample = firstPass[x][y];
                var colour = RequiresSuperSample(firstPass, x, y)
                    ? SuperSample(canvas, viewPort, scene, camera, recursionDepth, x, y)
                    : sample.Sample.Colour;
                canvas.PutPixel(sample.X, sample.Y, colour);
            }
        }
    }

    private static bool RequiresSuperSample((int X, int Y, (Colour Colour, Sphere? Sphere) Sample)[][] firstPass, int x, int y)
    {
        var identifier = GetIdentifier(firstPass, x, y);
        if (GetIdentifier(firstPass, x, y) != identifier)
        {
            return true;
        }
        if (GetIdentifier(firstPass, x, y) != identifier)
        {
            return true;
        }
        if (GetIdentifier(firstPass, x, y) != identifier)
        {
            return true;
        }
        if (GetIdentifier(firstPass, x, y) != identifier)
        {
            return true;
        }
        return false;
    }

    private static Guid? GetIdentifier((int X, int Y, (Colour Colour, Sphere? Sphere) Sample)[][] firstPass, int x, int y)
    {
        return x >= 0 && x < firstPass.Length && y >= 0 && y < firstPass[x].Length
            ? firstPass[x][y].Sample.Sphere?.Identifier
            : null;
    }

    public static Colour SuperSample(ICanvas canvas, ViewPort viewPort, Scene scene, Camera camera, int recursionDepth,
        double x, double y)
    {
        var colours = new List<Colour>
        {
            Sample(canvas, viewPort, scene, camera, recursionDepth, x - 0.25, y - 0.25).Colour,
            Sample(canvas, viewPort, scene, camera, recursionDepth, x - 0.25, y + 0.25).Colour,
            Sample(canvas, viewPort, scene, camera, recursionDepth, x + 0.25, y - 0.25).Colour,
            Sample(canvas, viewPort, scene, camera, recursionDepth, x + 0.25, y + 0.25).Colour
        };
        return colours.Average();
    }


    public static (Colour Colour, Sphere? Sphere) Sample(ICanvas canvas, ViewPort viewPort, Scene scene, Camera camera, int recursionDepth,
        double x, double y)
    {
        var direction = CanvasToViewport(canvas, viewPort, x, y);
        var ray = new Ray(camera.Location, direction);
        return TraceRay(ray, scene, viewPort.DistanceFromCamera, recursionDepth);
    }

    public static Vector CanvasToViewport(ICanvas canvas, ViewPort viewPort, double x, double y)
    {
        return new Vector
        (
            x * viewPort.Width / canvas.Width,
            y * viewPort.Height / canvas.Height,
            viewPort.DistanceFromCamera
        );
    }

    public static (Colour Colour, Sphere? Sphere) TraceRay(Ray ray, Scene scene, double tMin, int recursionDepth)
    {
        var closest = ray.GetClosestIntersection(scene.Spheres, tMin, Ray.Infinity);
        if (!closest.HasValue)
        {
            return (scene.BackgroundColour, null);
        }
        var intersection = ray.Origin + closest.Value.T * ray.Direction;
        var sphere = closest.Value.Sphere;
        var normal = (sphere as SphereBase).GetNormalAt(intersection);
        var intensity = scene.Lights.Sum(l => l.GetIntensityAt(intersection, normal, -ray.Direction, sphere.Specular, scene.Spheres));
        var localColour = sphere.Colour * intensity;
        if (recursionDepth <= 0 || sphere.Reflective <= 0)
        {
            return (localColour, sphere);
        }
        var reflection = new Ray(intersection, (-ray.Direction).Reflect(normal));
        var reflectedColour = TraceRay(reflection, scene, Ray.Epsilon, recursionDepth - 1).Colour;
        var finalColour = localColour * (1 - sphere.Reflective) + reflectedColour * sphere.Reflective;
        return (finalColour, sphere);
    }
}

