using System;
using ColorNote.Resources;

namespace ColorNote.Model;

public class Note : ValidationChecker
{
    private int _id;
    private string _title;
    private string _content;
    private Color _backgroundColor;

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
            if (string.IsNullOrWhiteSpace(_title))
                AddError($"{Strings.Note_TitleRequiredError}");
            else
                ClearErrors();
        }
    }
    
    public string Content
    {
        get => _content;
        set
        {
            _content = value;
            OnPropertyChanged();
        }
    }

    public DateTime Date { get; set; } = DateTime.Now;

    public Color BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            OnPropertyChanged();
        }
    }
}

public enum Color
{
    Red,
    Blue,
    Brown,
    Yellow,
    Gold,
    Black,
    Green
}