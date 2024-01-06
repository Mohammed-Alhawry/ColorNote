using System.Collections.ObjectModel;
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
    public string Title { get; set; }
    
    public string[] Colors { get; set; } = new string[] { "Green", "Blue", "Yellow", "Gold", "Black", "Brown", "White" };
    public string SelectedColor { get; set; } = "Blue";
    public string Content { get; set; }
    public DelegateCommand AddNoteInformationCommand { get; }
    public DelegateCommand LoadedCommand { get; }

    public AddNoteWindowViewModel(MainContext context)
    {
        _context = context;
        AddNoteInformationCommand = new DelegateCommand(SaveInformation);
        LoadedCommand = new DelegateCommand(WindowGotLoaded);
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
        var sender = parameter as Window;

        var isTitleEmpty = string.IsNullOrWhiteSpace(Title);

        if (!isTitleEmpty)
        {
            _context.Notes.Add(new Note() { Title = Title, Content = Content , BackgroundColor = SelectedColor});
            _context.SaveChanges();
            sender.Close();
        }
    }
}
