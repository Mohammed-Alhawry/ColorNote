using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using ColorNote.Command;
using ColorNote.Localization;
using MaterialDesignThemes.Wpf;

namespace ColorNote.ViewModel;

public class NavbarViewModel : ViewModelBase
{
    public NavbarViewModel()
    {
        IsToggleThemeButtonCheckedToDark = Properties.Settings.Default.Theme.Equals("Dark");
        SwitchThemeCommand = new DelegateCommand(SwitchTheme);
        ChangeLanguageToArabicCommand = new DelegateCommand(ChangeLanguageToArabic, CanClickArabic);
        ChangeLanguageToEnglishCommand = new DelegateCommand(ChangeLanguageToEnglish, CanClickEnglish);
    }

    public bool IsArabicChecked => !CanClickArabic(null);
    public bool IsEnglishChecked => !CanClickEnglish(null);
    public DelegateCommand ChangeLanguageToArabicCommand { get; }
    public DelegateCommand ChangeLanguageToEnglishCommand { get; }
    public DelegateCommand SwitchThemeCommand { get; }
    public bool IsToggleThemeButtonCheckedToDark { get; set; }


    public XmlLanguage Language => XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name);

    public FlowDirection FlowDirection => CultureInfo.CurrentCulture.TextInfo.IsRightToLeft
        ? FlowDirection.RightToLeft
        : FlowDirection.LeftToRight;

    private void ChangeLanguageToEnglish(object obj)
    {
        SetCulture("en");
        TranslationSource.Instance.CurrentCulture = CultureInfo.CurrentCulture;
        OnPropertyChanged(nameof(Language));
        OnPropertyChanged(nameof(FlowDirection));
        ChangeLanguageToArabicCommand.RaiseCanExecuteChanged();
        ChangeLanguageToEnglishCommand.RaiseCanExecuteChanged();
        ColorNote.Properties.Settings.Default.CultureName = CultureInfo.CurrentCulture.Name;
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
        SetCulture("ar");
        TranslationSource.Instance.CurrentCulture = CultureInfo.CurrentCulture;
        OnPropertyChanged(nameof(Language));
        OnPropertyChanged(nameof(FlowDirection));
        ChangeLanguageToArabicCommand.RaiseCanExecuteChanged();
        ChangeLanguageToEnglishCommand.RaiseCanExecuteChanged();
        ColorNote.Properties.Settings.Default.CultureName = CultureInfo.CurrentCulture.Name;
    }

    private void SetCulture(string cultureName)
    {
        var culture = new CultureInfo(cultureName);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
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
        ColorNote.Properties.Settings.Default.Theme = theme.GetBaseTheme().ToString();
    }

    public override Task LoadAsync()
    {
        return Task.CompletedTask;
    }
}