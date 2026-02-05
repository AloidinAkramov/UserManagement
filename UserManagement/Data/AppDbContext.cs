using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // IMPORTANT:
        // Email uniqueness is enforced on database level.
        // Application code relies on this constraint.
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}