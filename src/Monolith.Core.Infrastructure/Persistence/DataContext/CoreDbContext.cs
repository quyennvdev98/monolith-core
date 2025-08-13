using Microsoft.EntityFrameworkCore;

namespace Monolith.Core.Persistence.DataContext;

public sealed class CoreDbContext : DbContext
{
    public CoreDbContext(DbContextOptions<CoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure entity mappings here
        // Example: modelBuilder.Entity<YourEntity>().ToTable("YourTableName");
    }
}