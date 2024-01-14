using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация refresh токена.
/// </summary>
public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable(nameof(RefreshToken));

        builder.ConfigureBaseEntity();

        builder.HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId);

        builder.Property(x => x.Token)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.HasIndex(x => x.Token)
            .IsUnique();

        builder.Property(x => x.RememberMe)
            .HasColumnType("boolean")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.ExpiryDate)
            .HasColumnType("timestamptz")
            .IsRequired();
    }
}
