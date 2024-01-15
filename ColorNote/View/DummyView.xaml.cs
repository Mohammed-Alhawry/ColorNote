using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ColorNote.ViewModel;
using MaterialDesignThemes.Wpf;

namespace ColorNote.View;

public partial class DummyView : UserControl
{
    private DummyViewModel _viewModel;
    public DummyView()
    {
        InitializeComponent();
        _viewModel = new DummyViewModel();
        DataContext = _viewModel;
    }

    private void ToggleButton_Checked(object sender, System.Windows.RoutedEventArgs e)
    {
        var pallete = new PaletteHelper();
        var theme = pallete.GetTheme();
        theme.SetBaseTheme(BaseTheme.Light);
        pallete.SetTheme(theme);
    }

    private void ToggleButton_Unchecked(object sender, System.Windows.RoutedEventArgs e)
    {
        var pallete = new PaletteHelper();
        var theme = pallete.GetTheme();
        theme.SetBaseTheme(BaseTheme.Dark);
        pallete.SetTheme(theme);
    }
}