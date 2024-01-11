using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация связи многие-ко-многим между мероприятием и пользователем в качестве спикера.
/// </summary>
public class EventSpeakerConfiguration : IEntityTypeConfiguration<EventSpeaker>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<EventSpeaker> builder)
    {
        builder.ToTable(nameof(EventSpeaker));

        builder.HasKey(x => new { x.EventId, x.UserId });

        builder.Property(x => x.CreateDate)
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(x => x.UpdateDate)
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.HasOne(x => x.Event)
            .WithMany(x => x.EventSpeakers)
            .HasForeignKey(x => x.EventId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.EventSpeakers)
            .HasForeignKey(x => x.UserId);
    }
}
