using System.Windows;
using ColorNote.Command;
using ColorNote.Data;
using ColorNote.Model;
using Wpf.Ui.Controls;

namespace ColorNote.ViewModel;

public class EditNoteWindowViewModel
{
    private readonly MainContext _context;
    public Note Note { get; set; }
    public DelegateCommand UpdateNoteInformationCommand { get; set; }
    public DelegateCommand CancelCommand { get; set; }
    public DelegateCommand WindowGotLoadedCommand { get; set; }
    
    public EditNoteWindowViewModel(MainContext context, Note note)
    {
        _context = context;
        Note = note;
        Note.BeginEdit();
        WindowGotLoadedCommand = new DelegateCommand(WindowGotLoaded);
        UpdateNoteInformationCommand = new DelegateCommand(UpdateNoteInformation);
        
        CancelCommand = new DelegateCommand(Cancel);
    }

    
    private void UpdateNoteInformation(object parameter)
    {
        var editWindow = parameter as Window;

        if (string.IsNullOrWhiteSpace(Note.Title))
            return;
        Note.EndEdit();
        _context.SaveChanges();
        editWindow.Close();
    }
    
    private void Cancel(object parameter)
    {
        var editWindow = parameter as Window;
        Note.CancelEdit();
        _context.SaveChanges();
        editWindow.Close();
    }

    private void WindowGotLoaded(object parameter)
    {
        var editWindow = parameter as FrameworkElement;
        editWindow.Focus();
    }
}