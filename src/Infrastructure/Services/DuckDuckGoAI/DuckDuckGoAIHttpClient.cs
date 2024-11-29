using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using Infrastructure.Services.DuckDuckGoAI.Exceptions;
using Infrastructure.Services.DuckDuckGoAI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.DuckDuckGoAI;

/// <summary>
/// Http-клиент для работы с DuckDuckGo AI Chat.
/// </summary>
public class DuckDuckGoAIHttpClient : IDuckDuckGoAIHttpClient
{
    private const string VqdAcceptHeader = "X-Vqd-Accept";
    private const string VqdAcceptHeaderValue = "1";
    private const string Vqd4Header = "X-Vqd-4";

    private const string AIModel = "gpt-4o-mini";
    private const string UserRole = "user";

    private readonly ILogger<DuckDuckGoAIHttpClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly DuckDuckGoAIHttpClientOptions _options;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="logger">Логгер событий.</param>
    /// <param name="httpClient">Http-клиент сервиса DuckDuckGo AI Chat.</param>
    /// <param name="options">Опции DuckDuckGo AI Chat.</param>
    public DuckDuckGoAIHttpClient(
        ILogger<DuckDuckGoAIHttpClient> logger,
        HttpClient httpClient,
        IOptions<DuckDuckGoAIHttpClientOptions> options)
    {
        _logger = logger;
        _httpClient = httpClient;
        _options = options.Value;
    }

    /// <inheritdoc />
    public async Task<string> GetAnswerAsync(
        string request,
        CancellationToken cancellationToken = default)
    {
        _logger.LogTrace("<GetAnswerAsync>: {Request}", request);

        var getAnswerRequest = new GetAnswerDuckDuckGoAIRequest
        {
            Model = _options.Model,
            Messages =
            [
                new RequestMessageDuckDuckGoAIModel
                {
                    Role = UserRole,
                    Content = request
                }
            ]
        };

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, "chat")
        {
            Content = JsonContent.Create(getAnswerRequest)
        };

        var apiToken = await GetApiTokenAsync(cancellationToken);
        httpRequest.Headers.Add(Vqd4Header, apiToken);

        using var response = await _httpClient.SendAsync(httpRequest, cancellationToken);

        CheckHttpSuccessResponse(response);
        return await GetMessageAsync(response, cancellationToken);
    }

    private async Task<string> GetApiTokenAsync(CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "status");
        request.Headers.Add(VqdAcceptHeader, VqdAcceptHeaderValue);

        using var response = await _httpClient.SendAsync(request, cancellationToken);

        CheckHttpSuccessResponse(response);

        if (!response.Headers.TryGetValues(Vqd4Header, out var values))
        {
            _logger.LogError("DuckDuckGo AI Chat did not send api token");

            throw new DuckDuckGoAIRequestException(
                "DuckDuckGo AI Chat did not send api token",
                null,
                HttpStatusCode.InternalServerError);
        }

        return values.First();
    }

    private void CheckHttpSuccessResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("DuckDuckGo AI Chat status code unsuccessful");

            throw new DuckDuckGoAIRequestException(
                "DuckDuckGo AI Chat status code unsuccessful",
                null,
                response.StatusCode);
        }
    }

    private async Task<string> GetMessageAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        var responseString = new StringBuilder();

        using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        string? line;
        while ((line = await reader.ReadLineAsync(cancellationToken)) is not null)
        {
            if (line.StartsWith("data: "))
            {
                var jsonData = line.Substring("data: ".Length).Trim();

                if (jsonData == "[DONE]")
                {
                    break;
                }

                var responseObject = JsonSerializer.Deserialize<GetAnswerDuckDuckGoAIResponse>(jsonData);

                if (responseObject is not null && !string.IsNullOrEmpty(responseObject.Message))
                {
                    responseString.Append(responseObject.Message);
                }
            }
        }

        return responseString.ToString();
    }
}