using System.IO;
using ColorNote.Model;
using Microsoft.EntityFrameworkCore;

namespace ColorNote.Data;

public class MainContext : DbContext
{
    public DbSet<Note> Notes { get; set; }

    private string dataBasePath = Path.Combine(Directory.GetCurrentDirectory(), "notesAppDB.db");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={dataBasePath}");
    }
}
