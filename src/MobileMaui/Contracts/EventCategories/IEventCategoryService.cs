using MobileMaui.Contracts.EventCategories.Dto;

namespace MobileMaui.Contracts.EventCategories;

public interface IEventCategoryService
{
    /// <summary>
    /// Получает список категорий мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="sectionId">Уникальный идентификатор раздела мероприятия.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список категорий мероприятий.</returns>
    Task<List<EventCategoryDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        long? sectionId = null,
        CancellationToken cancellationToken = default);
}