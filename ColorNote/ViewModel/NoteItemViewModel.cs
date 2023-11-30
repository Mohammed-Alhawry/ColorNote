using System;
using System.Threading.Tasks;
using Accessibility;
using ColorNote.Model;

namespace ColorNote.ViewModel;

public class NoteItemViewModel : ViewModelBase
{
    private readonly Note _model;

    public NoteItemViewModel(Note model)
    {
        _model = model;
    }

    public int Id => _model.Id;

    public string Title
    {
        get => _model.Title;
        set
        {
            _model.Title = value;
            RaisePropertyChanged();
        }
    }

    public string Content
    {
        get => _model.Content;
        set
        {
            _model.Content = value;
            RaisePropertyChanged();
        }
    }

    public DateTime Date
    {
        get => _model.Date;
        set
        {
            _model.Date = value;
            RaisePropertyChanged();
        }
    }

    public override Task LoadAsync()
    {
        throw new NotImplementedException();
    }
}