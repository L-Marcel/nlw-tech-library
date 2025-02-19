using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Infrastructure.DataAccess;

public class TechLibraryDbContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        string directory = Directory.GetCurrentDirectory();
        string dbPath = Path.Combine(directory, "..", "TechLibrary.Infrastructure", "database.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}