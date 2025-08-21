using Microsoft.EntityFrameworkCore;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Persistence;

public sealed class CoreDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Policy> Policies { get; set; } = null!;
    public DbSet<RoleGroup> RoleGroups { get; set; } = null!;
    public DbSet<UserMapRoleGroup> UserMapRoleGroups { get; set; } = null!;
    public DbSet<PolicyMapRole> PolicyMapRoles { get; set; } = null!;
    public DbSet<RoleGroupMapRole> RoleGroupMapRoles { get; set; } = null!;
    public DbSet<RoleGroupClaim> RoleGroupClaims { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public CoreDbContext(DbContextOptions<CoreDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
       
    }
}