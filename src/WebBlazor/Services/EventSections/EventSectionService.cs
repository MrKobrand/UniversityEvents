using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebBlazor.Contracts.EventSections;
using WebBlazor.Contracts.EventSections.Dto;
using WebBlazor.Data;

namespace WebBlazor.Services.EventSections;

/// <summary>
/// Сервис для работы с разделами мероприятий.
/// </summary>
public class EventSectionService : IEventSectionService
{
    private readonly IHttpRepository _httpRepository;

    private const string EVENT_SECTIONS = "api/event-sections";

    /// <summary>
    ///  Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="httpRepository">Http-репозиторий.</param>
    public EventSectionService(IHttpRepository httpRepository)
    {
        _httpRepository = httpRepository;
    }

    /// <inheritdoc/>
    public Task<List<EventSectionDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["Limit"] = limit.HasValue ? limit.Value.ToString() : "25",
            ["Search"] = search ?? string.Empty
        };

        return _httpRepository.GetRequestAsync<List<EventSectionDto>>(
            EVENT_SECTIONS, queryParams, cancellationToken)!;
    }
}
