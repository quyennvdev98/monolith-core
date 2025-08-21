using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Infrastructure.Persistence.MappingConfigurations;

public class UserMapRoleGroupConfiguration : IEntityTypeConfiguration<UserMapRoleGroup>
{
    public void Configure(EntityTypeBuilder<UserMapRoleGroup> builder)
    {
        builder.ToTable("UserMapRoleGroups");

       
        builder.HasKey(x => new { x.UserId, x.RoleGroupId });

        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Value,
                value => new UserId(value));

        builder.Property(x => x.RoleGroupId)
            .HasConversion(
                id => id.Value,
                value => new RoleGroupId(value));

        // User 1 - N UserMapRoleGroup
        builder.HasOne(x => x.User)
            .WithMany(u => u.UserMapRoleGroups)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // RoleGroup 1 - N UserMapRoleGroup
        builder.HasOne(x => x.RoleGroup)
            .WithMany(rg => rg.UserMapRoleGroups)
            .HasForeignKey(x => x.RoleGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
