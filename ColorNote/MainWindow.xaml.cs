using System;
using System.Windows;
using ColorNote.ViewModel;

namespace ColorNote
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += OnLoaded;
            loginView.IsVisibleChanged += OnLoginViewVisibilityChanged;
        }

        private void OnLoginViewVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (loginView.Visibility == Visibility.Collapsed)
                mainApplicationView.Visibility = Visibility.Visible;
            else
                mainApplicationView.Visibility = Visibility.Collapsed;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
        }

        private void Window_Deactivated(object sender, System.EventArgs e)
        {
            var window = sender as Window;
            window.Opacity = 0.5;
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            var window = sender as Window;
            window.Opacity = 1;
        }
    }
}