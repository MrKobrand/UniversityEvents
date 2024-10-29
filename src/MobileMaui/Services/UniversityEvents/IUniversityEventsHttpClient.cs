using MobileMaui.Services.UniversityEvents.Models.EventCategories;
using MobileMaui.Services.UniversityEvents.Models.Events;
using MobileMaui.Services.UniversityEvents.Models.EventSections;

namespace MobileMaui.Services.UniversityEvents;

public interface IUniversityEventsHttpClient
{
    Task<IReadOnlyList<GetEventSectionsListUniversityEventsResponse>> GetEventSectionsListAsync(
        GetEventSectionsListUniversityEventsRequest eventSectionsListRequest,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<GetEventCategoriesListUniversityEventsResponse>> GetEventCategoriesListAsync(
        GetEventCategoriesListUniversityEventsRequest eventCategoriesListRequest,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<GetEventsListUniversityEventsResponse>> GetEventsListAsync(
        GetEventsListUniversityEventsRequest eventsListRequest,
        CancellationToken cancellationToken = default);

    Task<GetEventUniversityEventsResponse> GetEventAsync(
        long id,
        CancellationToken cancellationToken = default);

    Task<byte[]> GetImageAsync(long id, CancellationToken cancellationToken = default);
}