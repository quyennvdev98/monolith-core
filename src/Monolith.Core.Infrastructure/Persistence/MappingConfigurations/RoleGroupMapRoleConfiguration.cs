using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Infrastructure.Persistence.MappingConfigurations;


public class RoleGroupMapRoleConfiguration : IEntityTypeConfiguration<RoleGroupMapRole>
{
    public void Configure(EntityTypeBuilder<RoleGroupMapRole> builder)
    {
        builder.ToTable("RoleGroupMapRoles");

        builder.HasKey(x => new { x.RoleGroupId, x.RoleId });

        builder.Property(x => x.RoleGroupId)
            .HasConversion(
                id => id.Value,
                value => new RoleGroupId(value));

        builder.Property(x => x.RoleId)
            .HasConversion(
                id => id.Value,
                value => new RoleId(value));

        //RoleGroup 1 - N RoleGroupMapRole
        builder.HasOne(x => x.RoleGroup)
            .WithMany(rg => rg.RoleGroupMapRoles)
            .HasForeignKey(x => x.RoleGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        //Role 1 - N RoleGroupMapRole
        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

