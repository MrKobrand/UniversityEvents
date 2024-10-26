namespace MobileMaui.Services.UniversityEvents;

public interface IUniversityEventsResponseHandler
{
    Task<T> HandleResponse<T>(HttpResponseMessage response, CancellationToken cancellationToken = default);
}