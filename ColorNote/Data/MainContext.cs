using System.IO;
using ColorNote.Model;
using Microsoft.EntityFrameworkCore;

namespace ColorNote.Data;

public class MainContext : DbContext
{
    public DbSet<Note> Notes { get; set; }

    private string dataBasePath = Path.Combine(Directory.GetCurrentDirectory(), "ColorNoteDB.db");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={dataBasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Note>().Property(b => b.BackgroundColor).HasConversion<string>();
    }
}
