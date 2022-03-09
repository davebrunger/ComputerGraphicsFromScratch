namespace RayTracer.Lib;

public record Scene(ImmutableList<Sphere> Spheres, ImmutableList<ILight> Lights, Colour BackgroundColour);