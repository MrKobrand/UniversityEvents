using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces;

/// <summary>
/// Контекст базы данных.
/// </summary>
public interface IUniversityEventsDbContext
{
    /// <summary>
    /// Мероприятия.
    /// </summary>
    DbSet<Event> Events { get; }

    /// <summary>
    /// Категории мероприятий.
    /// </summary>
    DbSet<EventCategory> EventCategories { get; }

    /// <summary>
    /// Секции мероприятий.
    /// </summary>
    DbSet<EventSection> EventSections { get; }

    /// <summary>
    /// Пользователи.
    /// </summary>
    DbSet<User> Users { get; }

    /// <summary>
    /// Мероприятия и пользователи в качестве спикеров.
    /// </summary>
    DbSet<EventSpeaker> EventSpeakers { get; }

    /// <summary>
    /// Мероприятия и пользователи в качестве участников.
    /// </summary>
    DbSet<EventParticipant> EventParticipants { get; }

    /// <summary>
    /// Изображения.
    /// </summary>
    DbSet<Image> Images { get; }

    /// <summary>
    /// База данных.
    /// </summary>
    DatabaseFacade Database { get; }

    /// <summary>
    /// Сохраняет изменения в контексте.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Количество измененных сущностей.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
