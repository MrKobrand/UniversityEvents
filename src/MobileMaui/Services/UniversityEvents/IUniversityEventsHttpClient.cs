using MobileMaui.Services.UniversityEvents.Models;

namespace MobileMaui.Services.UniversityEvents;

public interface IUniversityEventsHttpClient
{
    Task<IReadOnlyList<GetEventSectionsListUniversityEventsResponse>> GetEventSectionsListAsync(
        GetEventSectionsListUniversityEventsRequest eventSectionsListRequest,
        CancellationToken cancellationToken = default);
}