using Microsoft.Extensions.Logging;
using MobileMaui.Contracts.EventCategories;
using MobileMaui.Contracts.EventCategories.Dto;
using MobileMaui.Services.UniversityEvents;
using MobileMaui.Services.UniversityEvents.Models.EventCategories;

namespace MobileMaui.Services.EventCategories;

public class EventCategoryService : IEventCategoryService
{
    private readonly ILogger<EventCategoryService> _logger;
    private readonly IUniversityEventsHttpClient _universityEventsHttpClient;

    public EventCategoryService(
        ILogger<EventCategoryService> logger,
        IUniversityEventsHttpClient universityEventsHttpClient)
    {
        _logger = logger;
        _universityEventsHttpClient = universityEventsHttpClient;
    }

    public async Task<List<EventCategoryDto>> GetListAsync(
        int? limit = null,
        string? search = null,
        long? sectionId = null,
        CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetListAsync>: {Limit}, {Search}, {SectionId}", limit, search, sectionId);

        var request = new GetEventCategoriesListUniversityEventsRequest
        {
            Limit = limit,
            Search = search,
            SectionId = sectionId
        };

        var eventCategoriesList = await _universityEventsHttpClient.GetEventCategoriesListAsync(
            request,
            cancellationToken);

        return eventCategoriesList
            .Select(x => new EventCategoryDto
            {
                Id = x.Id,
                Name = x.Name,
                Order = x.Order,
                SectionId = x.SectionId,
                SectionName = x.SectionName
            })
            .ToList();
    }
}