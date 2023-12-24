using ColorNote.Model;
using Microsoft.EntityFrameworkCore;

namespace ColorNote.Data;

public class MainContext : DbContext
{
    public DbSet<Note> Notes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=notesAppDB.db");
    }
}