using System.Windows;
using ColorNote.Data;
using ColorNote.Experimental_things;
using ColorNote.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using MaterialDesignThemes.Wpf;
using System;
using System.IO;
using ColorNote.PersistentSettings;
using System.Text.Json;

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
            services.AddTransient<DummyViewModel>();

            services.AddSingleton<MainContext>();
            services.AddSingleton<Settings>(provider =>
            {
                var fileName = "Settings.json";
                var settings = new Settings();
                if (!File.Exists(fileName))
                {
                    settings.MaterialDesignInXaml = new MaterialDesignInXamlSettings();
                    settings.MaterialDesignInXaml.Theme = "Light";
                    settings.IsToggleThemeButtonChecked = false;
                    JsonSerializer.Serialize<Settings>(settings);
                    return settings;
                }
                else
                {
                    settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(fileName));
                    return settings;
                }
            });

        }




        //        services.AddSingleton<Settings>(provider =>
        //            {
        //                var fileName = "Settings.json";
        //                var settings = new Settings();
        //                try
        //                {
        //                    settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(fileName));
        //                }
        //                catch (Exception e)
        //                {
        //                    Console.WriteLine($"Error while deserializing Settings.json: {e.Message}");
        //                }
        //return settings;
        //            });
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            var palette = new PaletteHelper();
            var theme = palette.GetTheme();

            
            BaseTheme settingsTheme = (BaseTheme)Enum.Parse(typeof(BaseTheme), _serviceProvider.GetService<Settings>().MaterialDesignInXaml.Theme);
            theme.SetBaseTheme(settingsTheme);
            palette.SetTheme(theme);

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();

        }
    }
}