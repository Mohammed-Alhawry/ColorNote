using System.Windows;
using ColorNote.Data;
using ColorNote.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using ColorNote.PersistentSettings;
using System.Text.Json;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Windows.Markup;

namespace ColorNote
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<NotesViewModel>();

            services.AddSingleton<MainContext>();
            services.AddSingleton<Settings>(provider =>
            {
                return GetUserSettingsObjectIfExistsOrCreate("Settings.json");
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetTheme(_serviceProvider.GetService<Settings>().MaterialDesignInXaml.Theme);
            SetCultureInfo(_serviceProvider.GetService<Settings>().ChosenCultureName);


            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();
        }

        private void SetCultureInfo(string cultureName)
        {
            var culture = new CultureInfo(cultureName);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }

        private void SetTheme(string chosenTheme)
        {
            var palette = new PaletteHelper();
            var theme = palette.GetTheme();


            BaseTheme settingsTheme = (BaseTheme)Enum.Parse(typeof(BaseTheme), chosenTheme);
            theme.SetBaseTheme(settingsTheme);
            palette.SetTheme(theme);
        }

        private CultureInfo GetAppropriateCultureInfo()
        {
            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
                return new CultureInfo("ar");
            return new CultureInfo("en");
        }

        private Settings GetUserSettingsObjectIfExistsOrCreate(string jsonFileName)
        {
            if (File.Exists(jsonFileName))
                return JsonSerializer.Deserialize<Settings>(File.ReadAllText(jsonFileName));

            var userSettings = new Settings();
            userSettings.MaterialDesignInXaml = new MaterialDesignInXamlSettings();
            userSettings.MaterialDesignInXaml.Theme = "Dark";
            userSettings.IsToggleThemeButtonCheckedToDark = true;
            userSettings.ChosenCultureName = GetAppropriateCultureInfo().Name;
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

            JsonSerializer.Serialize<Settings>(userSettings, options);
            return userSettings;
        }
    }
}