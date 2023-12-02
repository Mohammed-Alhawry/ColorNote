using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ColorNote.Data;
using ColorNote.ViewModel;

namespace ColorNote
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow(new MainWindowViewModel(new NotesViewModel(new NoteDataProvider())));
            mainWindow.Show();
        }
    }
}