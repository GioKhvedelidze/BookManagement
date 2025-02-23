using BookManagement.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DataAccess.Data;

public class AppIdentityDbContext : IdentityDbContext
{
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
}