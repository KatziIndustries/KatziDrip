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

    public static Color Parse(string rgbaString)
    {
        string[] values = rgbaString.Split(' ');

        if (values.Length != 4)
            throw new Exception($"RGBA string ({rgbaString}) wasn't in the right format.");

        return new(
            int.Parse(values[0]),
            int.Parse(values[1]),
            int.Parse(values[2]),
            int.Parse(values[3])
        );
    }
}
