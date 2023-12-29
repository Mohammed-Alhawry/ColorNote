using System.Threading.Tasks;
using ColorNote.Command;
using ColorNote.Data;

namespace ColorNote.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _selectedViewModel;

    public MainWindowViewModel(NotesViewModel notesViewModel, DummyViewModel dummyViewModel, NoteFieldsWindowViewModel noteFieldsWindowViewModel)
    {
        NotesViewModel = notesViewModel;
        DummyViewModel = dummyViewModel;
        SelectedViewModel = notesViewModel;
        NoteFieldsWindowViewModel = noteFieldsWindowViewModel;
        SelectViewModelCommand = new DelegateCommand(SelectViewModel, CanClick);
    }

    public NotesViewModel NotesViewModel { get; }
    public NoteFieldsWindowViewModel NoteFieldsWindowViewModel { get; }
    public  DummyViewModel DummyViewModel { get; }

    public DelegateCommand SelectViewModelCommand { get;}
    
    
    public ViewModelBase SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            RaisePropertyChanged();
            SelectViewModelCommand?.RaiseCanExecuteChanged();
        }
    }

    private bool CanClick(object parameter)
    {
        if (parameter is null || SelectedViewModel.GetType() == parameter.GetType())
            return false;

        return true;
    }
    
    public override async Task LoadAsync()
    {
        await SelectedViewModel.LoadAsync();
    }

    private async void SelectViewModel(object parameter)
    {
        SelectedViewModel = parameter as ViewModelBase;
        await LoadAsync();
    }
}