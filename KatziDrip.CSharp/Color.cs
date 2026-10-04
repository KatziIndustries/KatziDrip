namespace KatziDrip;

public readonly struct Color(int r, int g, int b, int a)
{
    public readonly int R = r;
    public readonly int G = g;
    public readonly int B = b;
    public readonly int A = a;

    public System.Drawing.Color ToSystemColor()
    {
        return System.Drawing.Color.FromArgb(A, R, G, B);
    }
}
