using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MobileMaui.Services.UniversityEvents.Exceptions;
using MobileMaui.Services.UniversityEvents.Models.EventCategories;
using MobileMaui.Services.UniversityEvents.Models.Events;
using MobileMaui.Services.UniversityEvents.Models.EventSections;

namespace MobileMaui.Services.UniversityEvents;

public class UniversityEventsHttpClient : IUniversityEventsHttpClient
{
    private readonly ILogger<UniversityEventsHttpClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly IUniversityEventsResponseHandler _universityEventsResponseHandler;

    public UniversityEventsHttpClient(
        ILogger<UniversityEventsHttpClient> logger,
        HttpClient httpClient,
        IUniversityEventsResponseHandler universityEventsResponseHandler)
    {
        _logger = logger;
        _httpClient = httpClient;
        _universityEventsResponseHandler = universityEventsResponseHandler;
    }

    public Task<IReadOnlyList<GetEventSectionsListUniversityEventsResponse>> GetEventSectionsListAsync(
        GetEventSectionsListUniversityEventsRequest eventSectionsListRequest,
        CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetEventSectionsListAsync>: {@Request}", eventSectionsListRequest);

        var queryParams = new KeyValuePair<string, string?>[]
        {
            new("limit", eventSectionsListRequest.Limit?.ToString()),
            new("search", eventSectionsListRequest.Search?.ToString())
        };

        var request = CreateGetRequest("api/event-sections", queryParams);

        return SendRequest<IReadOnlyList<GetEventSectionsListUniversityEventsResponse>>(
            request,
            cancellationToken);
    }

    public Task<IReadOnlyList<GetEventCategoriesListUniversityEventsResponse>> GetEventCategoriesListAsync(
        GetEventCategoriesListUniversityEventsRequest eventCategoriesListRequest,
        CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetEventCategoriesListAsync>: {@Request}", eventCategoriesListRequest);

        var queryParams = new KeyValuePair<string, string?>[]
        {
            new("limit", eventCategoriesListRequest.Limit?.ToString()),
            new("search", eventCategoriesListRequest.Search?.ToString()),
            new("sectionId", eventCategoriesListRequest.SectionId?.ToString())
        };

        var request = CreateGetRequest("api/event-categories", queryParams);

        return SendRequest<IReadOnlyList<GetEventCategoriesListUniversityEventsResponse>>(
            request,
            cancellationToken);
    }

    public Task<IReadOnlyList<GetEventsListUniversityEventsResponse>> GetEventsListAsync(
        GetEventsListUniversityEventsRequest eventsListRequest,
        CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetEventsListAsync>: {@Request}", eventsListRequest);

        var queryParams = new KeyValuePair<string, string?>[]
        {
            new("limit", eventsListRequest.Limit?.ToString()),
            new("search", eventsListRequest.Search?.ToString()),
            new("categoryId", eventsListRequest.CategoryId?.ToString())
        };

        var request = CreateGetRequest("api/events", queryParams);

        return SendRequest<IReadOnlyList<GetEventsListUniversityEventsResponse>>(
            request,
            cancellationToken);
    }

    public Task<GetEventUniversityEventsResponse> GetEventAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetEventAsync>: {Id}", id);

        var request = CreateGetRequest($"api/events/{id}");

        return SendRequest<GetEventUniversityEventsResponse>(
            request,
            cancellationToken);
    }

    public Task<byte[]> GetImageAsync(long id, CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetImageAsync>: {Id}", id);

        var request = CreateGetRequest($"api/images/{id}");

        return SendFileRequest(request, cancellationToken);
    }

    private HttpRequestMessage CreateGetRequest(
        string path,
        IEnumerable<KeyValuePair<string, string?>>? queryString = null)
    {
        var requestUri = queryString is null
            ? path
            : QueryHelpers.AddQueryString(path, queryString);

        return new HttpRequestMessage(HttpMethod.Get, requestUri);
    }

    private async Task<T> SendRequest<T>(
        HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);
            var result = await _universityEventsResponseHandler.HandleResponse<T>(response, cancellationToken);
            return result;
        }
        catch (Exception ex) when (ex is not UniversityEventsHttpRequestException)
        {
            _logger.LogError(ex, "Exception while sending university events request");
            throw new UniversityEventsHttpRequestException(ex.Message, ex, null);
        }
    }

    private async Task<byte[]> SendFileRequest(
        HttpRequestMessage request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);
            return await response.Content.ReadAsByteArrayAsync(cancellationToken);
        }
        catch (Exception ex) when (ex is not UniversityEventsHttpRequestException)
        {
            _logger.LogError(ex, "Exception while sending university events request");
            throw new UniversityEventsHttpRequestException(ex.Message, ex, null);
        }
    }
}