using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monolith.Core.Domain.Entities;

namespace Monolith.Core.Infrastructure.Persistence.MappingConfigurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, id => new UserId(id));

        builder.Property(e => e.CreatedAt)
            .IsRequired();
            
        builder.Property(e => e.UpdatedAt)
            .IsRequired(false);

        builder.Property(u => u.FirstName)
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .HasMaxLength(100);

        builder.Property(u => u.UserName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.NormalizedUserName)
            .HasMaxLength(256);

        builder.Property(u => u.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.NormalizedEmail)
            .HasMaxLength(256);

        builder.Property(u => u.EmailConfirmed)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(500);

        builder.Property(u => u.SecurityStamp)
            .HasMaxLength(500);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(50);

        builder.Property(u => u.PhoneNumberConfirmed)
            .HasDefaultValue(false);

        builder.Property(u => u.TwoFactorEnabled)
            .HasDefaultValue(false);

        builder.Property(u => u.LockoutEnabled)
            .HasDefaultValue(false);

        builder.Property(u => u.AccessFailedCount)
            .HasDefaultValue(0);

        builder.Property(u => u.SearchHint)
            .HasMaxLength(512);

        builder.Property(u => u.Avatar)
            .HasMaxLength(512);

        builder.Property(u => u.IsActivated)
            .IsRequired()
            .HasDefaultValue(true);


        builder.HasMany(u => u.UserMapRoleGroups)
            .WithOne(rg => rg.User)
            .HasForeignKey(rg => rg.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
