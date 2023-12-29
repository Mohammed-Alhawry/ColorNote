using System.Threading.Tasks;
using System.Windows;
using ColorNote.Command;
using ColorNote.Data;
using ColorNote.Model;

namespace ColorNote.ViewModel;

public class NoteFieldsWindowViewModel : ViewModelBase
{
    private readonly MainContext _context;
    public string Title { get; set; }
    public string Content { get; set; }
    public DelegateCommand AddNoteInformationCommand { get; }
    public NoteFieldsWindowViewModel(MainContext context)
    {
        _context = context;
        AddNoteInformationCommand = new DelegateCommand(SaveInformation);
    }
    
    public override Task LoadAsync()
    {
        return Task.CompletedTask;
    }
    
    private void SaveInformation(object parameter)
    {
        var sender = parameter as Window;
        
        var isTitleEmpty = string.IsNullOrWhiteSpace(Title);
        
        if (!isTitleEmpty)
        {
            _context.Notes.Add(new Note() { Title = Title, Content = Content });
            _context.SaveChanges();
            sender.Close();    
        }
    }
}