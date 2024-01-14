using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.EventSections.Dto;

namespace Application.Contracts.EventSections;

/// <summary>
/// Сервис для работы с разделами мероприятий.
/// </summary>
public interface IEventSectionService
{
    /// <summary>
    /// Получает раздел мероприятия по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Раздел мероприятия.</returns>
    Task<EventSectionDto?> GetAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает список разделов мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список разделов мероприятий.</returns>
    Task<List<EventSectionDto>> GetListAsync(int? limit, string? search, CancellationToken cancellationToken);

    /// <summary>
    /// Создает раздел мероприятия.
    /// </summary>
    /// <param name="name">Наименование раздела мероприятия.</param>
    /// <param name="description">Описание раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Созданный раздел мероприятия.</returns>
    Task<EventSectionDto> CreateAsync(string name, string? description, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет раздел мероприятия.
    /// </summary>
    /// <param name="id">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Удаленный раздел мероприятия.</returns>
    Task<EventSectionDto?> DeleteAsync(long id, CancellationToken cancellationToken);
}
