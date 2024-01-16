using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebBlazor.Contracts.Events;
using WebBlazor.Contracts.Events.Dto;
using WebBlazor.Data;

namespace WebBlazor.Services.Events;

/// <summary>
/// Сервис для работы с мероприятиями.
/// </summary>
public class EventService : IEventService
{
    private readonly IHttpRepository _httpRepository;

    private const string EVENTS = "api/events";
    private const string EVENT = "api/events/{0}";

    /// <summary>
    ///  Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="httpRepository">Http-репозиторий.</param>
    public EventService(IHttpRepository httpRepository)
    {
        _httpRepository = httpRepository;
    }

    /// <inheritdoc/>
    public Task<DetailedEventDto?> GetAsync(long id, CancellationToken cancellationToken = default)
    {
        return _httpRepository.GetRequestAsync<DetailedEventDto>(
            string.Format(EVENT, id),
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public Task<List<DetailedEventDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        long? categoryId = null,
        CancellationToken cancellationToken = default)
    {
        var queryParams = new Dictionary<string, string>
        {
            ["Limit"] = limit.HasValue ? limit.Value.ToString() : "25",
            ["Search"] = search ?? string.Empty,
            ["CategoryId"] = categoryId.HasValue ? categoryId.Value.ToString() : string.Empty
        };

        return _httpRepository.GetRequestAsync<List<DetailedEventDto>>(EVENTS, queryParams, cancellationToken)!;
    }
}
