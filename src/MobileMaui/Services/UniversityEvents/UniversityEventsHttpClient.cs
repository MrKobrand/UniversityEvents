using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using MobileMaui.Services.UniversityEvents.Exceptions;
using MobileMaui.Services.UniversityEvents.Models;

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
}