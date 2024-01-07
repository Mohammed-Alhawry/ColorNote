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
        DeleteNoteCommand = new DelegateCommand(DeleteNote);
    }

    public override async Task LoadAsync()
    {

    }

    private void AddNote(object parameter)
    {
        var addNoteWindow = new AddNoteWindow(_context);
        addNoteWindow.Owner = Application.Current.MainWindow;
        addNoteWindow.ShowDialog();
    }

    private void DeleteNote(object parameter)
    {

        var id = parameter as Nullable<int>;

        if (id is not null)
        {
            var note = _context.Notes.SingleOrDefault(x => x.Id == id);
            ArgumentNullException.ThrowIfNull(note);
            _context.Notes.Remove(note);
            _context.SaveChanges();
        }

    }

    private bool CanDelete(object parameter)
    {
        return SelectedNote is not null;
    }
}