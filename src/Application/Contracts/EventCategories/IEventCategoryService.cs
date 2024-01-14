using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.EventCategories.Dto;

namespace Application.Contracts.EventCategories;

/// <summary>
/// Сервис для работы с категориями мероприятий.
/// </summary>
public interface IEventCategoryService
{
    /// <summary>
    /// Получает категорию мероприятия по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Категория мероприятия.</returns>
    Task<DetailedEventCategoryDto?> GetAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает список категорий мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="sectionId">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список категорий мероприятий.</returns>
    Task<List<DetailedEventCategoryDto>> GetListAsync(
        int? limit,
        string? search,
        long? sectionId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Создает категорию мероприятия.
    /// </summary>
    /// <param name="name">Наименование категории мероприятия.</param>
    /// <param name="sectionId">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Созданная категория мероприятия.</returns>
    Task<EventCategoryDto> CreateAsync(string name, long sectionId, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет категорию мероприятия.
    /// </summary>
    /// <param name="id">Уникальный идентификатор категории мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Удаленная категория мероприятия.</returns>
    Task<EventCategoryDto?> DeleteAsync(long id, CancellationToken cancellationToken);
}
