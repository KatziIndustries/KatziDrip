namespace KatziDrip;

public struct Kolor(int r, int g, int b, int a)
{
    public int R = r;
    public int G = g;
    public int B = b;
    public int A = a;

    public readonly System.Drawing.Color ToColor()
    {
        return System.Drawing.Color.FromArgb(A, R, G, B);
    }

    public static Kolor FromRgba(string rgbaString)
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

    public static Kolor FromHex(string hex)
    {
        hex = hex.TrimStart('#');

        if (hex.Length != 6)
            throw new FormatException("Expected format: #RRGGBB");

        return new(
            Convert.ToByte(hex.Substring(0, 2), 16),
            Convert.ToByte(hex.Substring(2, 2), 16),
            Convert.ToByte(hex.Substring(4, 2), 16),
            255
        );
    }
}
