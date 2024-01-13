using System.Reflection;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// Контекст базы данных.
/// </summary>
public class UniversityEventsDbContext : DbContext, IUniversityEventsDbContext
{
    /// <inheritdoc/>
    public DbSet<Event> Events => Set<Event>();

    /// <inheritdoc/>
    public DbSet<EventCategory> EventCategories => Set<EventCategory>();

    /// <inheritdoc/>
    public DbSet<EventSection> EventSections => Set<EventSection>();

    /// <inheritdoc/>
    public DbSet<User> Users => Set<User>();

    /// <inheritdoc/>
    public DbSet<EventSpeaker> EventSpeakers => Set<EventSpeaker>();

    /// <inheritdoc/>
    public DbSet<EventParticipant> EventParticipants => Set<EventParticipant>();

    /// <inheritdoc/>
    public DbSet<Image> Images => Set<Image>();

    /// <inheritdoc/>
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    /// <summary>
    /// Конструктор, принимающий настройки контекста базы данных.
    /// </summary>
    /// <param name="options">Параметризованные настройки типа <see cref="UniversityEventsDbContext"/>.</param>
    public UniversityEventsDbContext(DbContextOptions<UniversityEventsDbContext> options)
        : base(options)
    {
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
