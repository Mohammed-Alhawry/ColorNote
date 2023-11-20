using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ColorNote.Data;
using ColorNote.Model;

namespace ColorNote.ViewModel;

public class NotesViewModel
{
    private readonly INoteDataProvider _noteDataProvider;
    public ObservableCollection<NoteItemViewModel> Notes { get; } = new();

    // public NoteItemViewModel SelectedNote;
    public NotesViewModel(INoteDataProvider noteDataProvider)
    {
        _noteDataProvider = noteDataProvider;
    }
    
    public async Task LoadAsync()
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

    public void AddNote()
    {
        var note = new Note() { Title = "New" };
        var noteViewModel = new NoteItemViewModel(note); 
        Notes.Add(noteViewModel);
        // SelectedNote = noteViewModel;
    }
}