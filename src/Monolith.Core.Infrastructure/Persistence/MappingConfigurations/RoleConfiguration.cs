using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Infrastructure.Persistence.MappingConfigurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasConversion(id => id.Value, value => new RoleId(value))
            .IsRequired();

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(r => r.NormalizedName)
            .HasMaxLength(256);

        builder.Property(r => r.Description)
            .HasMaxLength(512);

        builder.Property(r => r.IsActivated)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(r => r.IsRemoved)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(r => r.Creator)
            .WithMany()
            .HasForeignKey(r => r.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Updater)
            .WithMany()
            .HasForeignKey(r => r.UpdaterId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasMany(r => r.PolicyMapRoles)
            .WithOne(pm => pm.Role)
            .HasForeignKey(pm => pm.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}