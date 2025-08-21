using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Infrastructure.Persistence.MappingConfigurations;

public class RoleGroupClaimConfiguration : IEntityTypeConfiguration<RoleGroupClaim>
{
    public void Configure(EntityTypeBuilder<RoleGroupClaim> builder)
    {
        builder.ToTable("RoleGroupClaims");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,      
                value => new RoleGroupClaimId(value)) 
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Key)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Value)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasOne(x => x.RoleGroup)
            .WithMany(rg => rg.RoleGroupClaims)
            .HasForeignKey(x => x.RoleGroupId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
