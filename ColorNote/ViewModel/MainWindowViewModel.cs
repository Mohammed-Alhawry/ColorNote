using System.Threading.Tasks;
using ColorNote.Command;
using ColorNote.Data;

namespace ColorNote.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NotesViewModel _notesViewModel;
    private ViewModelBase _selectedViewModel;
    private readonly DummyViewModel _dummyViewModel;

    public MainWindowViewModel(NotesViewModel notesViewModel) : this()
    {
        _notesViewModel = notesViewModel;
        SelectedViewModel = notesViewModel;
    }

    public MainWindowViewModel()
    {
        ChangeViewToNotesViewCommand = new DelegateCommand(ChangeViewToNotesView, CanClickViewMenuItem);
        ChangeViewToDummyViewCommand = new DelegateCommand(ChangeViewToDummyView, CanClickDummyMenuItem);
    }


    public MainWindowViewModel(DummyViewModel dummyViewModel) : this()
    {
        _dummyViewModel = dummyViewModel;
        SelectedViewModel = _dummyViewModel;
    }

    public DelegateCommand ChangeViewToDummyViewCommand { get; private set; }
    public DelegateCommand ChangeViewToNotesViewCommand { get; private set; }

    public ViewModelBase SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            RaisePropertyChanged();
            ChangeViewToDummyViewCommand.RaiseCanExecuteChanged();
            ChangeViewToNotesViewCommand.RaiseCanExecuteChanged();
        }
    }

    public override async Task LoadAsync()
    {
        await SelectedViewModel.LoadAsync();
    }

    private void ChangeViewToDummyView(object? parameter)
    {
        if (SelectedViewModel is DummyViewModel)
            return;

        SelectedViewModel = new DummyViewModel();
        SelectedViewModel.LoadAsync();
    }

    private void ChangeViewToNotesView(object? parameter)
    {
        if (SelectedViewModel is NotesViewModel)
            return;

        SelectedViewModel = new NotesViewModel(new NoteDataProvider());
        SelectedViewModel.LoadAsync();
    }

    private bool CanClickDummyMenuItem(object arg)
    {
        return SelectedViewModel is NotesViewModel;
    }

    private bool CanClickViewMenuItem(object arg)
    {
        return SelectedViewModel is DummyViewModel;
    }
}