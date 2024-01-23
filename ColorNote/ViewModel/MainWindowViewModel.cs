using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using ColorNote.Command;
using ColorNote.Localization;
using ColorNote.PersistentSettings;
using MaterialDesignThemes.Wpf;

namespace ColorNote.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _selectedViewModel;
    private Settings _settings;

    public MainWindowViewModel(NotesViewModel notesViewModel, Settings settings, NavbarViewModel navbarViewModel)
    {
        _settings = settings;
        NotesViewModel = notesViewModel;
        NavbarViewModel = navbarViewModel;
        SelectedViewModel = notesViewModel;
        SelectViewModelCommand = new DelegateCommand(SelectViewModel, CanClick);
        ClosingWindowCommand = new DelegateCommand(OnClosingWindow);
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
        SetChosenValuesForSettingsObject();
        SaveUserSettingsToJsonFile("Settings.json");
    }

    private void SaveUserSettingsToJsonFile(string jsonFileName)
    {
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        var finalJsonString = JsonSerializer.Serialize(_settings, options);
        File.WriteAllText(jsonFileName, finalJsonString);
    }

    private void SetChosenValuesForSettingsObject()
    {
        var palette = new PaletteHelper();
        var theme = palette.GetTheme();
        var finalTheme = theme.GetBaseTheme().ToString();

        _settings.ChosenCultureName = CultureInfo.CurrentCulture.Name;
        _settings.MaterialDesignInXaml.Theme = finalTheme;
        _settings.IsToggleThemeButtonCheckedToDark = NavbarViewModel.IsToggleThemeButtonCheckedToDark;
    }

    private async void SelectViewModel(object parameter)
    {
        SelectedViewModel = parameter as ViewModelBase;
        await LoadAsync();
    }
}