using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация раздела мероприятий.
/// </summary>
public class EventSectionConfiguration : IEntityTypeConfiguration<EventSection>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<EventSection> builder)
    {
        builder.ToTable(nameof(EventSection));

        builder.ConfigureBaseEntity();

        builder.Property(x => x.Name)
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnType("varchar(256)");

        builder.Property(x => x.Order)
            .HasColumnType("smallint")
            .HasDefaultValue(0)
            .IsRequired();
    }
}
