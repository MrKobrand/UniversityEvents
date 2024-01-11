using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация связи многие-ко-многим между мероприятием и пользователем в качестве участника.
/// </summary>
public class EventParticipantConfiguration : IEntityTypeConfiguration<EventParticipant>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<EventParticipant> builder)
    {
        builder.ToTable(nameof(EventParticipant));

        builder.HasKey(x => new { x.EventId, x.UserId });

        builder.Property(x => x.CreateDate)
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(x => x.UpdateDate)
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.HasOne(x => x.Event)
            .WithMany(x => x.EventParticipants)
            .HasForeignKey(x => x.EventId);

        builder.HasOne(x => x.User)
            .WithMany(x => x.EventParticipants)
            .HasForeignKey(x => x.UserId);
    }
}
