using System.Windows.Controls;
using ColorNote.ViewModel;

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
}