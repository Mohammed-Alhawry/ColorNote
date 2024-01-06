using System.Windows;
using System.Windows.Media;
using ColorNote.Data;
using ColorNote.View;
using ColorNote.ViewModel;

namespace ColorNote.Controls;

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