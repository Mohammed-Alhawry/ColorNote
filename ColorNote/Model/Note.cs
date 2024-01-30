using System;
using System.ComponentModel;
using ColorNote.Resources;

namespace ColorNote.Model;

public class Note : ValidationChecker, IEditableObject
{
    private int _id;
    private string _title;
    private string _content;
    private Color _backgroundColor;
    private Note _tempValues;

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

    public void BeginEdit()
    {
        _tempValues = new Note();
        _tempValues.Id = Id;
        _tempValues.BackgroundColor = BackgroundColor;
        _tempValues.Title = Title;
        _tempValues.Content = Content;
    }

    public void CancelEdit()
    {
        Id = _tempValues.Id;
        BackgroundColor = _tempValues.BackgroundColor;
        Title = _tempValues.Title;
        Content = _tempValues.Content;
    }

    public void EndEdit()
    {
        _tempValues = null;
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
    Green,
    Purple,
    Pink
}