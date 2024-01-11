using System.Windows;
using ColorNote.Data;
using ColorNote.Model;
using ColorNote.ViewModel;

namespace ColorNote.Windows;

public partial class EditNoteWindow : Window
{
    
    private readonly EditNoteWindowViewModel _editNoteWindowViewModel;
    public EditNoteWindow(MainContext context, Note selectedNoteToEdit)
    {
        InitializeComponent();
        _editNoteWindowViewModel = new EditNoteWindowViewModel(context, selectedNoteToEdit);
        DataContext = _editNoteWindowViewModel;
    }
}