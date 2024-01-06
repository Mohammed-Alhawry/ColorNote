using System;
using System.Windows.Media;
namespace ColorNote.Model;

public class Note : ValidationChecker
{
    private int _id;
    private string _title;
    private string _content;
    private string _backgroundColor;

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            OnPropertyChanged();
        } 
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged();
            if(string.IsNullOrWhiteSpace(_title))
                AddError("Note title is required");
            else
                ClearErrors();
        }
    }

    public string? Content
    {
        get => _content;
        set
        {
            _content = value;
            OnPropertyChanged();
        }
    }

    public DateTime Date { get; set; } = DateTime.Now;

    public string BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            OnPropertyChanged();
        }
    }
}