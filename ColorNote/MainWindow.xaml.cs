using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ColorNote.Data;
using ColorNote.ViewModel;

namespace ColorNote
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel(new NotesViewModel(new NoteDataProvider()));
            DataContext = _viewModel;
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadAsync();
        }

        private void NotesMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if(_viewModel.SelectedViewModel is NotesViewModel)
                return;

            _viewModel.SelectedViewModel = new NotesViewModel(new NoteDataProvider());
            _viewModel.LoadAsync();
        }

        private void DummyMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if(_viewModel.SelectedViewModel is DummyViewModel)
                return;

            _viewModel.SelectedViewModel = new DummyViewModel();
            _viewModel.LoadAsync();
        }
    }
}