namespace Graphics.Common;

public interface ICanvas
{
    int Height { get; }
    int Width { get; }

    void PutPixel(int x, int y, Colour colour);
}

