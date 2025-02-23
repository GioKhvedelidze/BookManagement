using BookManagement.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Book> Books { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted);
        
    }
}