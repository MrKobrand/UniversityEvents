using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

/// <summary>
/// Контекст базы данных.
/// </summary>
public interface IApplicationDbContext
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
    /// Сохраняет изменения в контексте.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Количество измененных сущностей.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
