using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using ColorNote.Command;
using ColorNote.Data;
using ColorNote.Model;
using System.Windows.Media;
using SQLitePCL;

namespace ColorNote.ViewModel;

public class AddNoteWindowViewModel : ViewModelBase
{
    private readonly MainContext _context;

    public Note Note { get; set; }

    public string[] Colors { get; set; } =
        new string[] { "Green", "Blue", "Yellow", "Gold", "Black", "Brown", "White" };

    
    
    public DelegateCommand AddNoteInformationCommand { get; }
    public DelegateCommand LoadedCommand { get; }

    public AddNoteWindowViewModel(MainContext context)
    {
        _context = context;
        AddNoteInformationCommand = new DelegateCommand(SaveInformation);
        LoadedCommand = new DelegateCommand(WindowGotLoaded);
        Note = new Note()
        {
            BackgroundColor = "Yellow",
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
        Note.Title = "hi";
        Note.Title = "";
        var sender = parameter as Window;

        var isTitleEmpty = string.IsNullOrWhiteSpace(Note.Title);

        if (!isTitleEmpty)
        {
            _context.Notes.Add(Note);
            _context.SaveChanges();
            sender.Close();
        }
    }
}