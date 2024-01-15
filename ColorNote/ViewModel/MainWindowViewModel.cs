using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using ColorNote.Command;
using ColorNote.Data;
using ColorNote.PersistentSettings;
using MaterialDesignThemes.Wpf;

namespace ColorNote.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _selectedViewModel;

    public MainWindowViewModel(NotesViewModel notesViewModel, DummyViewModel dummyViewModel)
    {
        NotesViewModel = notesViewModel;
        DummyViewModel = dummyViewModel;
        SelectedViewModel = notesViewModel;

        SelectViewModelCommand = new DelegateCommand(SelectViewModel, CanClick);
        ClosingWindowCommand = new DelegateCommand(OnClosingWindow);
        SwitchThemeCommand = new DelegateCommand(SwitchTheme);
    }

    private void OnClosingWindow(object obj)
    {
        var palette = new PaletteHelper();
        var theme = palette.GetTheme();
        var finalTheme = theme.GetBaseTheme().ToString();

        Settings settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText("Settings.json"));
        settings.MaterialDesignInXaml.Theme = finalTheme;
        var finalJsonString = JsonSerializer.Serialize(settings);
        File.WriteAllText("Settings.json", finalJsonString);
    }

    private void SwitchTheme(object paremter)
    {
        var senderToggleButton = paremter as ToggleButton;

        var palete = new PaletteHelper();
        var theme = palete.GetTheme();

        if (theme.GetBaseTheme() == BaseTheme.Light)
            theme.SetBaseTheme(BaseTheme.Dark);
        else
            theme.SetBaseTheme(BaseTheme.Light);
        
        
        palete.SetTheme(theme);
    }

    public NotesViewModel NotesViewModel { get; }
    public AddNoteWindowViewModel AddNoteWindowViewModel { get; }
    public DummyViewModel DummyViewModel { get; }

    public DelegateCommand SelectViewModelCommand { get; }
    public DelegateCommand ClosingWindowCommand { get; }
    public DelegateCommand SwitchThemeCommand { get; }
    public DelegateCommand SwitchThemeToDarkCommand { get; }


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

    private bool CanClick(object parameter)
    {
        if (parameter is null || SelectedViewModel.GetType() == parameter.GetType())
            return false;

        return true;
    }

    public override async Task LoadAsync()
    {
        await SelectedViewModel.LoadAsync();
    }

    private async void SelectViewModel(object parameter)
    {
        SelectedViewModel = parameter as ViewModelBase;
        await LoadAsync();
    }
}