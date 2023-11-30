using System.Threading.Tasks;

namespace ColorNote.ViewModel;

public class MainWindowViewModel : ViewModelBase
{
    private readonly NotesViewModel _notesViewModel;
    private ViewModelBase _selectedViewModel;
    private readonly DummyViewModel _dummyViewModel;
    public MainWindowViewModel(NotesViewModel notesViewModel)
    {
        _notesViewModel = notesViewModel;
        SelectedViewModel = notesViewModel;
    }

    public MainWindowViewModel(DummyViewModel dummyViewModel)
    {
        _dummyViewModel = dummyViewModel;
        SelectedViewModel = _dummyViewModel;
    }

    public ViewModelBase SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            RaisePropertyChanged();
        }
    }

    public override async Task LoadAsync()
    {
        await SelectedViewModel.LoadAsync();
    }
}