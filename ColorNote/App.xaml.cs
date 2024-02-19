using System.Windows;
using ColorNote.Data;
using ColorNote.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using MaterialDesignThemes.Wpf;
using System;
using System.Globalization;

//using ColorNote.Properties;

namespace ColorNote
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public ServiceProvider ServiceProvider => _serviceProvider;

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
            services.AddTransient<NavbarViewModel>();

            services.AddSingleton<MainContext>();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {

                base.OnStartup(e);
                if (ColorNote.Properties.Settings.Default.CultureName.Equals("None"))
                    ColorNote.Properties.Settings.Default.CultureName = CultureInfo.CurrentCulture.Name;

                SetTheme(ColorNote.Properties.Settings.Default.Theme);
                SetCultureInfo(ColorNote.Properties.Settings.Default.CultureName);


                var mainWindow = _serviceProvider.GetService<MainWindow>();
                mainWindow?.Show();
            }
            catch
            {
                MessageBox.Show("Something went wrong, try contacting the application developer to fix the issue", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        private void SetCultureInfo(string cultureName)
        {
            var culture = new CultureInfo(cultureName);
            CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture = culture;
        }

        private void SetTheme(string chosenTheme)
        {
            var palette = new PaletteHelper();
            var theme = palette.GetTheme();


            BaseTheme settingsTheme = (BaseTheme)Enum.Parse(typeof(BaseTheme), chosenTheme);
            theme.SetBaseTheme(settingsTheme);
            palette.SetTheme(theme);
        }

        
    }
}