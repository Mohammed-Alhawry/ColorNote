using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ColorNote.Model;

namespace ColorNote.Data;

public interface INoteDataProvider
{
    Task<IEnumerable<Note>> GetAllAsync();
}
public class NoteDataProvider : INoteDataProvider
{
    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        await Task.Delay(100);

        return new List<Note>()
        {
            new Note() { Id = 1, Title = "First Title", Content = "Content", Date = DateTime.Now },
            new Note() { Id = 2, Title = "Second Title", Content = "Content", Date = DateTime.Now },
            new Note() { Id = 3, Title = "Third Title", Content = "Content", Date = DateTime.Now }
        };
    }
}