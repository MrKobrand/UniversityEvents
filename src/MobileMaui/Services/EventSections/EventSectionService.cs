using Microsoft.Extensions.Logging;
using MobileMaui.Contracts.EventSections;
using MobileMaui.Contracts.EventSections.Dto;
using MobileMaui.Services.UniversityEvents;
using MobileMaui.Services.UniversityEvents.Models;

namespace MobileMaui.Services.EventSections;

public class EventSectionService : IEventSectionService
{
    private readonly ILogger<EventSectionService> _logger;
    private readonly IUniversityEventsHttpClient _universityEventsHttpClient;

    public EventSectionService(
        ILogger<EventSectionService> logger,
        IUniversityEventsHttpClient universityEventsHttpClient)
    {
        _logger = logger;
        _universityEventsHttpClient = universityEventsHttpClient;
    }

    public async Task<List<EventSectionDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetListAsync>: {Limit}, {Search}", limit, search);

        var request = new GetEventSectionsListUniversityEventsRequest
        {
            Limit = limit,
            Search = search
        };

        var eventSectionsList = await _universityEventsHttpClient.GetEventSectionsListAsync(request, cancellationToken);

        return eventSectionsList
            .Select(x => new EventSectionDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Order = x.Order
            })
            .ToList();
    }
}