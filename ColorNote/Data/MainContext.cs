using System;
using System.IO;
using ColorNote.Model;
using Microsoft.EntityFrameworkCore;

namespace ColorNote.Data;

public class MainContext : DbContext
{
    public DbSet<Note> Notes { get; set; }

    public string DataBasePath { get; set; }
    public MainContext()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DataBasePath = Path.Join(path, "ColorNoteDB.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DataBasePath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Note>().Property(b => b.BackgroundColor).HasConversion<string>();
    }
}
