namespace Graphics.Common;

public record Colour(int Red, int Green, int Blue)
{
    public Colour Scale(double scale) => new(Clamp(scale * Red), Clamp(scale * Green), Clamp(scale * Blue));
    public Colour Add(Colour other) => new(Clamp(other.Red + Red), Clamp(other.Green + Green), Clamp(other.Blue + Blue));
    private static int Clamp(double value) => Math.Max(0, Math.Min(255, (int)Math.Round(value)));

    public static Colour operator +(Colour c1, Colour c2) => c1.Add(c2);
    public static Colour operator *(double scale, Colour colour) => colour.Scale(scale);
    public static Colour operator *(Colour colour, double scale) => colour.Scale(scale);
}

public static class ColourExtensions
{
    public static Colour Average(this IEnumerable<Colour> colours)
    {
        var colourCount = 0;
        var red = 0L;
        var green = 0L;
        var blue = 0L;
        foreach (var colour in colours)
        {
            colourCount++;
            red += colour.Red;
            green += colour.Green;
            blue += colour.Blue;
        }
        return new Colour((int)(red / colourCount), (int)(green / colourCount), (int)(blue / colourCount));
    }
}

public static class Colours
{
    public static readonly Colour Black = new(0, 0, 0);
    public static readonly Colour Blue = new(0, 0, 255);
    public static readonly Colour Green = new(0, 255, 0);
    public static readonly Colour Red = new(255, 0, 0);
    public static readonly Colour White = new(255, 255, 255);
    public static readonly Colour Yellow = new(255, 255, 0);
}

