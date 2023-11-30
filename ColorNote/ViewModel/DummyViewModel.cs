using System.Threading.Tasks;

namespace ColorNote.ViewModel;

public class DummyViewModel : ViewModelBase
{
    public string Text { get; set; } = "i am just an another window";

    public override Task LoadAsync()
    {
        return Task.CompletedTask;
    }
}