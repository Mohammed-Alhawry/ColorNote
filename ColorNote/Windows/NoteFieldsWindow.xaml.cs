using System.Windows;
using System.Windows.Media;
using ColorNote.Data;
using ColorNote.View;
using ColorNote.ViewModel;

namespace ColorNote.Controls;

public partial class NoteFieldsWindow : Window
{
    private NoteFieldsWindowViewModel _noteFieldsWindowViewModel;

    public NoteFieldsWindow(MainContext mainContext)
    {
        InitializeComponent();
        _noteFieldsWindowViewModel = new NoteFieldsWindowViewModel(mainContext);
        DataContext = _noteFieldsWindowViewModel;
    }
    
}