using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using MobileMaui.Services.UniversityEvents.Exceptions;

namespace MobileMaui.Services.UniversityEvents;
public class UniversityEventsResponseHandler : IUniversityEventsResponseHandler
{
    private readonly ILogger<UniversityEventsResponseHandler> _logger;

    public UniversityEventsResponseHandler(ILogger<UniversityEventsResponseHandler> logger)
    {
        _logger = logger;
    }

    public async Task<T> HandleResponse<T>(
        HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(
                "Invalid university events response with {StatusCode} status code",
                response.StatusCode);

            throw new UniversityEventsHttpRequestException(
                "Invalid university events response",
                null,
                response.StatusCode);
        }

        var responseContent = await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);

        if (responseContent is null)
        {
            _logger.LogError("Invalid university events response");

            throw new UniversityEventsHttpRequestException(
                "Invalid university events response",
                null,
                response.StatusCode);
        }

        var errors = new List<ValidationResult>(0);
        Validator.TryValidateObject(responseContent, new ValidationContext(responseContent), errors, true);

        if (errors.Any())
        {
            _logger.LogWarning(
                "Invalid university events response {@ResponseContent}, {@Errors}",
                responseContent,
                errors);

            throw new UniversityEventsHttpRequestException(
                "Invalid university events response",
                null,
                response.StatusCode);
        }

        return responseContent;
    }
}