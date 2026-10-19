namespace KatziDrip;

public static class Drip
{
    public static string DefaultThemeName => GetDefaultThemeName();

    public const string KATZI_THEME_NAME = "Katzi";
    public const string FILE_EXTENSION = ".kolor";

    public static readonly string ConfigPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KatziDrip");
    public static string DefaultThemePath => Path.Combine(ConfigPath, $"{DefaultThemeName}{FILE_EXTENSION}");

    public static readonly string KatziThemePath = Path.Combine(ConfigPath, $"{KATZI_THEME_NAME}{FILE_EXTENSION}");

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
                case "FontFamily":
                    FontFamily = value;
                    break;

                case "AccentColor":
                    AccentColor = Kolor.Parse(value);
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

        if (!File.Exists(KatziThemePath))
        {
            using (var fs = File.Create(KatziThemePath)) { }
            File.WriteAllText(KatziThemePath, DefaultTheme.Theme);
        }
        else
        {
            string fileContent = File.ReadAllText(KatziThemePath);
            if (fileContent != DefaultTheme.Theme)
            {
                File.WriteAllText(KatziThemePath, DefaultTheme.Theme);
            }
        }
    }

    public static bool ThemeExists(string themeName)
    {
        return File.Exists(Path.Combine(ConfigPath, themeName + FILE_EXTENSION));
    }

    private static string GetDefaultThemeName()
    {
        CreateConfigDirIfNotExists();

        string defaultThemePath = Path.Combine(ConfigPath, "DefaultTheme.txt");

        if (!File.Exists(defaultThemePath))
        {
            using (var fs = File.Create(defaultThemePath)) { }
            File.WriteAllText(defaultThemePath, "Katzi");
        }

        string themeName = File.ReadAllText(defaultThemePath).Trim();

        if (ThemeExists(themeName))
            return themeName;
        else
            return "Katzi";
    }

    public static string FontFamily { get; private set; } = "";

    public static Kolor BaseColor { get; private set; }
    public static Kolor LightColor { get; private set; }

    public static Kolor VeryLightColor { get; private set; }
    public static Kolor DarkColor { get; private set; }
    public static Kolor AccentColor { get; private set; }

    public static readonly Kolor White = new(255, 255, 255, 255);
    public static readonly Kolor Black = new(0, 0, 0, 255);
}
