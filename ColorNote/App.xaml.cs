using System.Windows;
using ColorNote.Data;
using ColorNote.Experimental_things;
using ColorNote.ViewModel;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddTransient<AddNoteWindowViewModel>();
            services.AddSingleton<MainContext>();
            
            services.AddTransient<INoteDataProvider, NoteDataProvider>();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            //var iconwindow = new IconWindow();
            //iconwindow.Show();

            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow?.Show();

        }
    }
}