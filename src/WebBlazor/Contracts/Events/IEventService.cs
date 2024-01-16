using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebBlazor.Contracts.Events.Dto;

namespace WebBlazor.Contracts.Events;

/// <summary>
/// Сервис для работы с мероприятиями.
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Получает мероприятие по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Мероприятие.</returns>
    Task<DetailedEventDto?> GetAsync(long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает список мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="categoryId">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список мероприятий.</returns>
    Task<List<DetailedEventDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        long? categoryId = null,
        CancellationToken cancellationToken = default);
}
