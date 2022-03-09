namespace Graphics.Common;

public static class Utilities
{
    public static IEnumerable<double> Interpolate(int i0, double d0, int i1, double d1)
    {
        if (i0 == i1)
        {
            yield return d0;
            yield break;
        }
        var a = (d1 - d0) / (i1 - i0);
        var d = d0;
        for(var i = i0; i <= i1; i++)
        {
            yield return d;
            d += a;
        }
    }
}

