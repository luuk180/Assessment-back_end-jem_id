using Assessment_back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment_back_end.Data;

public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    public DbSet<Artikel> Artikelen { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artikel>()
            .HasKey(a => a.Code);
    }
}