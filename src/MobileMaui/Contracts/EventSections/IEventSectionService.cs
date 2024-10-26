using MobileMaui.Contracts.EventSections.Dto;

namespace MobileMaui.Contracts.EventSections;

public interface IEventSectionService
{
    /// <summary>
    /// Получает список разделов мероприятий.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список разделов мероприятий.</returns>
    Task<List<EventSectionDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        CancellationToken cancellationToken = default);
}