using System.Windows;
using ColorNote.Data;
using ColorNote.ViewModel;

namespace ColorNote.Windows;

public partial class AddNoteWindow : Window
{
    private AddNoteWindowViewModel _AddNoteWindowViewModel;

    public AddNoteWindow(MainContext mainContext)
    {
        InitializeComponent();
        _AddNoteWindowViewModel = new AddNoteWindowViewModel(mainContext);
        DataContext = _AddNoteWindowViewModel;
    }
}