using System;

namespace ColorNote.Model;

public class Note
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}