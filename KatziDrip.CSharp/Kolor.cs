namespace Katzi.Drip;

public struct Kolor(byte r, byte g, byte b, byte a)
{
    public byte R = r;
    public byte G = g;
    public byte B = b;
    public byte A = a;

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
            byte.Parse(values[0]),
            byte.Parse(values[1]),
            byte.Parse(values[2]),
            byte.Parse(values[3])
        );
    }

    public static Kolor FromHex(string hex)
    {
        hex = hex.TrimStart('#');

        if (hex.Length == 6)
        {
            return new(
                Convert.ToByte(hex.Substring(0, 2), 16),
                Convert.ToByte(hex.Substring(2, 2), 16),
                Convert.ToByte(hex.Substring(4, 2), 16),
                255
            );
        }
        else if (hex.Length == 8)
        {
            return new(
                Convert.ToByte(hex.Substring(0, 2), 16),
                Convert.ToByte(hex.Substring(2, 2), 16),
                Convert.ToByte(hex.Substring(4, 2), 16),
                Convert.ToByte(hex.Substring(6, 2), 16)
            );
        }

        throw new FormatException("Expected format: #RRGGBB or #RRGGBBAA");
    }
}
