using System.Windows;
using ColorNote.Command;
using ColorNote.Constants;
using ColorNote.Data;
using ColorNote.Model;
using Wpf.Ui.Controls;

namespace ColorNote.ViewModel;

public class EditNoteWindowViewModel
{
    private readonly MainContext _context;
    public Note Note { get; set; }
    public Note OldNote { get; set; }
    public DelegateCommand UpdateNoteInformationCommand { get; set; }
    public DelegateCommand CancelCommand { get; set; }
    public DelegateCommand WindowGotLoadedCommand { get; set; }
    
    
    public string[] Colors => ComboBoxConstants.Colors;

    public EditNoteWindowViewModel(MainContext context, Note note)
    {
        _context = context;
        OldNote = new Note() { Title = note.Title, BackgroundColor = note.BackgroundColor, Content = note.Content};
        Note = note;
        
        WindowGotLoadedCommand = new DelegateCommand(WindowGotLoaded);
        UpdateNoteInformationCommand = new DelegateCommand(UpdateNoteInformation);
        CancelCommand = new DelegateCommand(Cancel);
    }

    private void UpdateNoteInformation(object parameter)
    {
        var editWindow = parameter as Window;

        if(string.IsNullOrWhiteSpace(Note.Title))
            return;

        _context.SaveChanges();
        editWindow.Close();
    }
    
    private void Cancel(object parameter)
    {
        var editWindow = parameter as Window;
        Note.BackgroundColor = OldNote.BackgroundColor;
        Note.Title = OldNote.Title;
        Note.Content = OldNote.Content;

        _context.SaveChanges();
        editWindow.Close();
    }

    private void WindowGotLoaded(object parameter)
    {
        var editWindow = parameter as FrameworkElement;
        editWindow.Focus();
    }
    
    
    
}