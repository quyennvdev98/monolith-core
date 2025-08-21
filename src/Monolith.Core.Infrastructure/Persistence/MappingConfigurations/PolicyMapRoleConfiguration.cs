using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Infrastructure.Persistence.MappingConfigurations;

public sealed class PolicyMapRoleConfiguration : IEntityTypeConfiguration<PolicyMapRole>
{
    public void Configure(EntityTypeBuilder<PolicyMapRole> builder)
    {
        builder.ToTable("PolicyMapRoles");

        builder.HasKey(pm => new { pm.PolicyId, pm.RoleId });

        builder.Property(pm => pm.PolicyId)
            .HasConversion(id => id.Value, value => new PolicyId(value));

        builder.Property(pm => pm.RoleId)
            .HasConversion(id => id.Value, value => new RoleId(value));

        builder.HasOne(pm => pm.Policy)
            .WithMany(p => p.PolicyMapRoles)
            .HasForeignKey(pm => pm.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pm => pm.Role)
            .WithMany(r => r.PolicyMapRoles)
            .HasForeignKey(pm => pm.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}