using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ColorNote.Command;
using ColorNote.Data;
using ColorNote.Model;
using Microsoft.EntityFrameworkCore;

namespace ColorNote.ViewModel;

public class NotesViewModel : ViewModelBase
{
    private readonly INoteDataProvider _noteDataProvider;
    private NoteItemViewModel _selectedNote;

    public ObservableCollection<NoteItemViewModel> Notes { get; } = new();
    public NoteItemViewModel SelectedNote
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
    
    public NotesViewModel(INoteDataProvider noteDataProvider)
    {
        _noteDataProvider = noteDataProvider;
        AddNoteCommand = new DelegateCommand(AddNote);
        DeleteNoteCommand = new DelegateCommand(DeleteNote, CanDelete);
    }
    

    public override async Task LoadAsync()
    {
        if (Notes.Any())
            return;

        var notes = await _noteDataProvider.GetAllAsync();
        if(notes is null)
            return;
        
        foreach (var note in notes)
        {
            Notes.Add(new NoteItemViewModel(note));
        }
    }

    private void AddNote(object parameter)
    {
        var note = new Note() { Title = "New" };
        var noteViewModel = new NoteItemViewModel(note); 
        Notes.Add(noteViewModel);
        // SelectedNote = noteViewModel;
    }

    private void DeleteNote(object parameter)
    {
        if (SelectedNote is not null)
        {
            Notes.Remove(SelectedNote);
            SelectedNote = null;
        }
    }
    
    private bool CanDelete(object parameter)
    {
        return SelectedNote is not null;
    }
}