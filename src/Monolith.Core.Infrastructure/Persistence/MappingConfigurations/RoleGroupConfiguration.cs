namespace Monolith.Core.Infrastructure.Persistence.MappingConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monolith.Core.Domain.Entities;

public sealed class RoleGroupConfiguration : IEntityTypeConfiguration<RoleGroup>
{
    public void Configure(EntityTypeBuilder<RoleGroup> builder)
    {
        builder.ToTable("RoleGroups");

        builder.HasKey(rg => rg.Id);

        builder.Property(rg => rg.Id)
            .HasConversion(id => id.Value, value => new RoleGroupId(value));

        builder.Property(rg => rg.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(rg => rg.Description)
            .HasMaxLength(500);

        builder.Property(rg => rg.SearchHint)
            .HasMaxLength(200);

        builder.Property(rg => rg.CreatedTime)
            .IsRequired();

        builder.Property(rg => rg.UpdatedTime);

        builder.Property(rg => rg.IsDefault)
            .IsRequired();

        builder.Property(rg => rg.IsActivated)
            .IsRequired();

        builder.Property(rg => rg.IsRemoved)
            .IsRequired();

        builder.HasOne(rg => rg.Creator)
            .WithMany()
            .HasForeignKey(rg => rg.CreatorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(rg => rg.Updater)
            .WithMany()
            .HasForeignKey(rg => rg.UpdaterId)
            .OnDelete(DeleteBehavior.Restrict);

        // 1 RoleGroup -> N RoleGroupMapRole
        builder.HasMany(rg => rg.RoleGroupMapRoles)
            .WithOne(rmr => rmr.RoleGroup)
            .HasForeignKey(rmr => rmr.RoleGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1 RoleGroup -> N UserMapRoleGroup
        builder.HasMany(rg => rg.UserMapRoleGroups)
            .WithOne(umrg => umrg.RoleGroup)
            .HasForeignKey(umrg => umrg.RoleGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // 1 RoleGroup -> N RoleGroupClaim
        builder.HasMany(rg => rg.RoleGroupClaims)
            .WithOne(rgc => rgc.RoleGroup)
            .HasForeignKey(rgc => rgc.RoleGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
