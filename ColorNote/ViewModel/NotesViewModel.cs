using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ColorNote.Command;
using ColorNote.Data;
using ColorNote.Controls;
using ColorNote.Model;
using Microsoft.EntityFrameworkCore;

namespace ColorNote.ViewModel;

public class NotesViewModel : ViewModelBase
{
    private readonly MainContext _context;
    
    private Note _selectedNote;

    public ObservableCollection<Note> Notes { get; } 
    public Note SelectedNote
    {
        get => _selectedNote;
        set
        {
            _selectedNote = value;
            DeleteNoteCommand.RaiseCanExecuteChanged();
        } 
    }

    public DelegateCommand AddNoteCommand { get; }
    public DelegateCommand DeleteNoteCommand { get; }
    
    public NotesViewModel(INoteDataProvider noteDataProvider, MainContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
        
        _context.Notes.Load();
        Notes = _context.Notes.Local.ToObservableCollection();
        
        AddNoteCommand = new DelegateCommand(AddNote);
        DeleteNoteCommand = new DelegateCommand(DeleteNote, CanDelete);
    }

    public override async Task LoadAsync()
    {
     
    }

    private void AddNote(object parameter)
    {
        var parentWindow = parameter as Window;
        var noteFieldsWindow = new NoteFieldsWindow();

        noteFieldsWindow.Owner = parentWindow;
        noteFieldsWindow.ShowDialog();



        //var note = new Note() { Title = "New" };
        //Notes.Add(note);

        //_context.Notes.Add(note);
        //_context.SaveChanges();
        // SelectedNote = noteViewModel;
    }

    private void DeleteNote(object parameter)
    {
        if (SelectedNote is not null)
        {
            _context.Notes.Remove(SelectedNote);
            _context.SaveChanges();
            
            Notes.Remove(SelectedNote);
            SelectedNote = null;
        }
    }
    
    private bool CanDelete(object parameter)
    {
        return SelectedNote is not null;
    }
}