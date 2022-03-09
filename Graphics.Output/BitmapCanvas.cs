namespace Graphics.Output;

public class BitmapCanvas : ICanvas, IDisposable
{
    private readonly Image<Rgba32> bitmap;
    private bool disposedValue;

    public BitmapCanvas(int width, int height)
    {
        bitmap = new Image<Rgba32>(width, height);
    }

    public int Height => bitmap.Height;
    public int Width => bitmap.Width;
    public void PutPixel(int x, int y, Colour colour)
    {
        bitmap[Width / 2 + x, Height / 2 - (y + 1)] = new Rgba32(colour.Red / 255F, colour.Green / 255F, colour.Blue / 255F);
    }

    public Task SaveAsync(string path, CancellationToken cancellationToken)
    {
        return bitmap.SaveAsync(path, cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                bitmap.Dispose();
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

