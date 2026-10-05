using System.Runtime.InteropServices;

namespace KatziDrip;

public static class Drip
{
    public const string DEFAULT_THEME_NAME = "Katzi";
    public const string FILE_EXTENSION = ".kolor";

    public static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KatziDrip");
    public static readonly string DefaultThemePath = Path.Combine(ConfigPath, $"{DEFAULT_THEME_NAME}{FILE_EXTENSION}");

    /// <summary>
    /// Loads the theme with the specified name.
    /// The name is decided by the name of the file without its extension.
    /// </summary>
    public static void Load(string themeName)
    {
        CreateConfigDirIfNotExists();

        string fileContent = File.ReadAllText(Path.Combine(ConfigPath, $"{themeName}{FILE_EXTENSION}"));
        string[] lines = fileContent.Split('\n');

        int index = 0;
        foreach (string line in lines)
        {
            if (line == string.Empty)
                continue;

            if (!line.Contains('='))
                throw new InvalidDataException($"Line {index} didn't contain a '='");

            string varName = line[0..line.IndexOf('=')];
            string value = line[(line.IndexOf('=') + 1)..line.Length];

            switch (varName)
            {
                case "FontName":
                    FontName = value;
                    break;

                case "SmallFontName":
                    SmallFontName = value;
                    break;

                case "BaseColor":
                    BaseColor = Kolor.Parse(value);
                    break;

                case "LightColor":
                    LightColor = Kolor.Parse(value);
                    break;

                case "VeryLightColor":
                    VeryLightColor = Kolor.Parse(value);
                    break;

                case "DarkColor":
                    DarkColor = Kolor.Parse(value);
                    break;
            }

            index++;
        }
    }

    private static void CreateConfigDirIfNotExists()
    {
        if (!Directory.Exists(ConfigPath))
        {
            Directory.CreateDirectory(ConfigPath);
        }

        if (!File.Exists(DefaultThemePath))
        {
            using (var fs = File.Create(DefaultThemePath)) { }
            File.WriteAllText(DefaultThemePath, DefaultTheme.Theme);
        }
    }

    public static string FontName { get; private set; } = "";
    public static string SmallFontName { get; private set; } = "";

    public static Kolor BaseColor { get; private set; }
    public static Kolor LightColor { get; private set; }
    public static Kolor VeryLightColor { get; private set; }
    public static Kolor DarkColor { get; private set; }

    public static readonly Kolor White = new(255, 255, 255, 255);
    public static readonly Kolor Black = new(0, 0, 0, 255);

    public static readonly Kolor RoyalBlue = new(48, 92, 222, 255);
}
