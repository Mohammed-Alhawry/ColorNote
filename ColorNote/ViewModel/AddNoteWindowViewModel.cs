using System;
using System.Threading.Tasks;
using System.Windows;
using ColorNote.Command;
using ColorNote.Data;
using ColorNote.Model;
using ColorNote.Windows;
using Color = ColorNote.Model.Color;

namespace ColorNote.ViewModel;

public class AddNoteWindowViewModel : ViewModelBase
{
    private readonly MainContext _context;

    public Note Note { get; set; }
    
    public DelegateCommand AddNoteInformationCommand { get; }
    
    public DelegateCommand WindowGotLoadedCommand { get; }

    public AddNoteWindowViewModel(MainContext context)
    {
        _context = context;
        AddNoteInformationCommand = new DelegateCommand(SaveInformation);
        
        WindowGotLoadedCommand = new DelegateCommand(WindowGotLoaded);
        Note = new Note()
        {
            BackgroundColor = Color.Gold,
            Date = DateTime.Now
        };
    }

    private void WindowGotLoaded(object obj)
    {
        var titleBox = obj as FrameworkElement;
        titleBox?.Focus();
    }

    public override Task LoadAsync()
    {
        return Task.CompletedTask;
    }

    private void SaveInformation(object parameter)
    {
        var sender = parameter as AddNoteWindow;
        
        if (string.IsNullOrWhiteSpace(Note.Title))
        {
            Note.Title = "  ";
            Note.Title = "";

            return;
        }
        
        var isTitleEmpty = string.IsNullOrWhiteSpace(Note.Title);
        if (!isTitleEmpty)
        {
            _context.Notes.Add(Note);
            _context.SaveChanges();
            sender.Close();
        }
    }
}