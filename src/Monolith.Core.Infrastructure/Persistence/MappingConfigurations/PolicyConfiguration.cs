using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Infrastructure.Persistence.MappingConfigurations;

public sealed class PolicyConfiguration : IEntityTypeConfiguration<Policy>
{
    public void Configure(EntityTypeBuilder<Policy> builder)
    {
        builder.ToTable("Policies");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => new PolicyId(value));

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.Resource)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Action)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Condition)
            .HasMaxLength(500);

        builder.Property(p => p.IsActive)
            .IsRequired();

        // Quan hệ 1 Policy -> N PolicyMapRole
        builder.HasMany(p => p.PolicyMapRoles)
            .WithOne(pm => pm.Policy)
            .HasForeignKey(pm => pm.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        //Indexes
        builder.HasIndex(x => new { x.Resource, x.Action });
    }
}