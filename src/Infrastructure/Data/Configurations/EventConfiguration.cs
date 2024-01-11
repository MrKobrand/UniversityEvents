using Domain.Entities;
using Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация мероприятия.
/// </summary>
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable(nameof(Event));

        builder.ConfigureBaseEntity();

        builder.Property(x => x.Type)
            .HasColumnType("smallint")
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(x => x.Duration)
            .HasConversion(new TimeSpanToTicksConverter());

        builder.Property(x => x.Place)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(x => x.Subject)
            .HasColumnType("varchar(256)")
            .IsRequired();

        builder.Property(x => x.Announcement)
            .HasColumnType("varchar(256)");

        builder.Property(x => x.Content)
            .HasColumnType("text");

        builder.HasOne(x => x.PreviewImage)
            .WithOne(x => x.Event)
            .HasForeignKey<Event>(x => x.PreviewImageId);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Events)
            .HasForeignKey(x => x.CategoryId);

        builder.HasOne(x => x.Author)
            .WithMany(x => x.EventsAsAuthor)
            .HasForeignKey(x => x.AuthorId);
    }
}
