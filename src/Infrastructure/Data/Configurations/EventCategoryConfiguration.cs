using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация категории мероприятий.
/// </summary>
public class EventCategoryConfiguration : IEntityTypeConfiguration<EventCategory>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<EventCategory> builder)
    {
        builder.ToTable(nameof(EventCategory));

        builder.ConfigureBaseEntity();

        builder.Property(x => x.Name)
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(x => x.Order)
            .HasColumnType("smallint")
            .HasDefaultValue(0)
            .IsRequired();

        builder.HasOne(x => x.Section)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.SectionId);
    }
}
