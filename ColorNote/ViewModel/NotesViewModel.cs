using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ColorNote.Command;
using ColorNote.Data;
using ColorNote.Windows;
using ColorNote.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace ColorNote.ViewModel;

public class NotesViewModel : ViewModelBase
{
    private readonly MainContext _context;

    private Note _selectedNote;

    public ObservableCollection<Note> Notes { get; private set; }
    public Note SelectedNote
    {
        get => _selectedNote;
        set
        {
            _selectedNote = value;
            DeleteNoteCommand.RaiseCanExecuteChanged();
        }
    }


    public DelegateCommand EditNoteInformationCommand { get; }
    public DelegateCommand AddNoteCommand { get; }
    public DelegateCommand DeleteNoteCommand { get; }

    public NotesViewModel(MainContext context)
    {
        _context = context;
        
        EditNoteInformationCommand = new DelegateCommand(EditNoteInformation);
        AddNoteCommand = new DelegateCommand(AddNote);
        DeleteNoteCommand = new DelegateCommand(DeleteNote);
    }

    public override async Task LoadAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await _context.Notes.LoadAsync();
        
        Notes = _context.Notes.Local.ToObservableCollection();
        OnPropertyChanged(nameof(Notes));
    }

    private void AddNote(object parameter)
    {
        var addNoteWindow = new AddNoteWindow(_context)
        {
            Owner = Application.Current.MainWindow
        };

        addNoteWindow.FlowDirection = addNoteWindow.Owner.FlowDirection;
        addNoteWindow.ShowDialog();
    }

    private void EditNoteInformation(object parameter)
    {
        var selectedNoteToEdit = parameter as Note;
        var editWindow = new EditNoteWindow(_context, selectedNoteToEdit);
        editWindow.Owner = Application.Current.MainWindow;
        editWindow.FlowDirection = editWindow.Owner.FlowDirection;
        editWindow.ShowDialog();
    }

    private void DeleteNote(object parameter)
    {
        var note = parameter as Note;

        if (note is not null)
        {
            var messageBoxText = "Are you sure that you want to delete this note?";
            var caption = "Delete Note";
            var messageBoxButton = MessageBoxButton.YesNo;
            var messageBoxIcon = MessageBoxImage.Warning;
            var result = MessageBox.Show(messageBoxText, caption, messageBoxButton, messageBoxIcon);
            if (result == MessageBoxResult.No) return;

            _context.Notes.Remove(note);
            _context.SaveChanges();
        }

    }

    private bool CanDelete(object parameter)
    {
        return SelectedNote is not null;
    }
}