using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebBlazor.Contracts.EventCategories;
using WebBlazor.Contracts.EventCategories.Dto;
using WebBlazor.Data;

namespace WebBlazor.Services.EventCategories;

/// <summary>
/// Сервис для работы с категориями мероприятий.
/// </summary>
public class EventCategoryService : IEventCategoryService
{
    private readonly IHttpRepository _httpRepository;

    private const string EVENT_CATEGORIES = "api/event-categories";

    /// <summary>
    ///  Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="httpRepository">Http-репозиторий.</param>
    public EventCategoryService(IHttpRepository httpRepository)
    {
        _httpRepository = httpRepository;
    }

    /// <inheritdoc/>
    public Task<List<DetailedEventCategoryDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        long? sectionId = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["Limit"] = limit.HasValue ? limit.Value.ToString() : "25",
            ["Search"] = search ?? string.Empty,
            ["SectionId"] = sectionId.HasValue ? sectionId.Value.ToString() : string.Empty
        };

        return _httpRepository.GetRequestAsync<List<DetailedEventCategoryDto>>(
            EVENT_CATEGORIES, queryParams, cancellationToken)!;
    }
}
