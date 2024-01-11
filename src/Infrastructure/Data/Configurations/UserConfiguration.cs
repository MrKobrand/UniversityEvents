using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация пользователя.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        builder.ConfigureBaseEntity();

        builder.Property(x => x.FirstName)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(x => x.MiddleName)
            .HasColumnType("varchar(256)");

        builder.Property(x => x.Role)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(x => x.Password)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(x => x.PasswordSalt)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.HasOne(x => x.AvatarImage)
            .WithOne(x => x.User)
            .HasForeignKey<User>(x => x.AvatarId);
    }
}
