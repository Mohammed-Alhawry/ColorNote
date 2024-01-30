using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using ColorNote.Command;
using MaterialDesignThemes.Wpf;

namespace ColorNote.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _selectedViewModel;
    

    public MainWindowViewModel(NotesViewModel notesViewModel, NavbarViewModel navbarViewModel)
    {
        
        NotesViewModel = notesViewModel;
        NavbarViewModel = navbarViewModel;
        SelectedViewModel = notesViewModel;
        SelectViewModelCommand = new DelegateCommand(SelectViewModel, CanClick);
        ClosingWindowCommand = new DelegateCommand(OnClosingWindow);
        var hi = (App)Application.Current;
    }

    public NavbarViewModel NavbarViewModel { get; }
    public NotesViewModel NotesViewModel { get; }
    public DelegateCommand SelectViewModelCommand { get; }

    public DelegateCommand ClosingWindowCommand { get; }
    
    

    public ViewModelBase SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            OnPropertyChanged();
            SelectViewModelCommand?.RaiseCanExecuteChanged();
            
        }
    }

    public override async Task LoadAsync()
    {
        await SelectedViewModel.LoadAsync();
    }

    private bool CanClick(object parameter)
    {
        if (parameter is null || SelectedViewModel.GetType() == parameter.GetType())
            return false;

        return true;
    }


    private void OnClosingWindow(object obj)
    {
        Properties.Settings.Default.Save();
    }

    
    private async void SelectViewModel(object parameter)
    {
        SelectedViewModel = parameter as ViewModelBase;
        await LoadAsync();
    }
}