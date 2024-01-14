using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация изображения.
/// </summary>
public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable(nameof(Image));

        builder.ConfigureBaseEntity();

        builder.Property(x => x.FileName)
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(x => x.ContentType)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(x => x.StorageType)
            .HasColumnType("smallint")
            .IsRequired();
    }
}
