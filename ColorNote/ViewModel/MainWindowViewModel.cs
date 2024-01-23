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

    public MainWindowViewModel(NotesViewModel notesViewModel, Settings settings)
    {
        _settings = settings;
        IsToggleThemeButtonChecked = _settings.IsToggleThemeButtonCheckedToDark;
        NotesViewModel = notesViewModel;

        SelectedViewModel = notesViewModel;
        SelectViewModelCommand = new DelegateCommand(SelectViewModel, CanClick);
        ClosingWindowCommand = new DelegateCommand(OnClosingWindow);
        SwitchThemeCommand = new DelegateCommand(SwitchTheme);
        ChangeLanguageToArabicCommand = new DelegateCommand(ChangeLanguageToArabic, CanClickArabic);
        ChangeLanguageToEnglishCommand = new DelegateCommand(ChangeLanguageToEnglish, CanClickEnglish);
    }

    public bool IsArabicChecked => !CanClickArabic(null);
    public bool IsEnglishChecked => !CanClickEnglish(null);
    public NotesViewModel NotesViewModel { get; }
    public DelegateCommand SelectViewModelCommand { get; }
    public DelegateCommand ChangeLanguageToArabicCommand { get; }
    public DelegateCommand ChangeLanguageToEnglishCommand { get; }
    public DelegateCommand ClosingWindowCommand { get; }
    public DelegateCommand SwitchThemeCommand { get; }
    public bool IsToggleThemeButtonChecked { get; set; }


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

    public XmlLanguage Language => XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);

    public FlowDirection FlowDirection => CultureInfo.CurrentCulture.TextInfo.IsRightToLeft
        ? FlowDirection.RightToLeft
        : FlowDirection.LeftToRight;

    public override async Task LoadAsync()
    {
        await SelectedViewModel.LoadAsync();
    }

    private void ChangeLanguageToEnglish(object obj)
    {
        var culture = new CultureInfo("en");
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        TranslationSource.Instance.CurrentCulture = culture;
        OnPropertyChanged(nameof(Language));
        OnPropertyChanged(nameof(FlowDirection));
        ChangeLanguageToArabicCommand.RaiseCanExecuteChanged();
        ChangeLanguageToEnglishCommand.RaiseCanExecuteChanged();
    }

    private bool CanClickArabic(object parameter)
    {
        return !CultureInfo.CurrentCulture.Name.StartsWith("ar");
    }

    private bool CanClickEnglish(object parameter)
    {
        return !CultureInfo.CurrentCulture.Name.StartsWith("en");
    }

    private void ChangeLanguageToArabic(object obj)
    {
        var culture = new CultureInfo("ar");
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        TranslationSource.Instance.CurrentCulture = culture;
        OnPropertyChanged(nameof(Language));
        OnPropertyChanged(nameof(FlowDirection));
        ChangeLanguageToArabicCommand.RaiseCanExecuteChanged();
        ChangeLanguageToEnglishCommand.RaiseCanExecuteChanged();
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
        _settings.IsToggleThemeButtonCheckedToDark = IsToggleThemeButtonChecked;
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

    private async void SelectViewModel(object parameter)
    {
        SelectedViewModel = parameter as ViewModelBase;
        await LoadAsync();
    }
}