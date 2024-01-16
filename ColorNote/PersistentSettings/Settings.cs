using System.IO;
using System.Text.Json;

namespace ColorNote.PersistentSettings;

public class Settings
{
    public MaterialDesignInXamlSettings MaterialDesignInXaml { get; set; }
    public bool IsToggleThemeButtonChecked { get; set; }
}

public class MaterialDesignInXamlSettings
{
    public string Theme { get; set; }
}