using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace WebBlazor.Data;

/// <summary>
/// Http-репозиторий.
/// </summary>
public class HttpRepository : IHttpRepository
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="client">Http-клиент.</param>
    public HttpRepository(HttpClient client)
    {
        _client = client;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        _jsonOptions.Converters.Add(new JsonStringEnumConverter());
    }

    /// <inheritdoc/>
    public virtual async Task<Stream> GetFileRequestAsync(
        string route,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var queryString = queryParams is null ? route : QueryHelpers.AddQueryString(route, queryParams!);
        var response = await _client.GetAsync(queryString, cancellationToken);

        await EnsureSuccessStatusCodeAsync(response, cancellationToken);

        return await _client.GetStreamAsync(queryString, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetRequestAsync<T>(
        string route,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
        where T : class
    {
        var queryString = queryParams is null ? route : QueryHelpers.AddQueryString(route, queryParams!);
        var response = await _client.GetAsync(queryString, cancellationToken);

        await EnsureSuccessStatusCodeAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<T>(_jsonOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task PatchRequestAsync(string route, object body, CancellationToken cancellationToken = default)
    {
        var response = await _client.PatchAsync(route, JsonContent.Create(inputValue: body, options: _jsonOptions), cancellationToken);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResponse?> PatchRequestAsync<TResponse>(
        string route,
        object body,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        var response = await _client.PatchAsync(route, JsonContent.Create(inputValue: body, options: _jsonOptions), cancellationToken);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task PostRequestAsync(string route, object body, CancellationToken cancellationToken = default)
    {
        var response = await _client.PostAsJsonAsync(route, body, _jsonOptions, cancellationToken);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResponse?> PostRequestAsync<TResponse>(
        string route,
        object body,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        var response = await _client.PostAsJsonAsync(route, body, _jsonOptions, cancellationToken);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResponse?> PostRequestAsync<TResponse>(
        string route,
        HttpContent body,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        var response = await _client.PostAsync(route, body, cancellationToken);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<string> PostRequestRawResultAsync(
        string route,
        object body,
        CancellationToken cancellationToken = default)
    {
        var response = await _client.PostAsJsonAsync(route, body, _jsonOptions, cancellationToken);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken);

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task PutRequestAsync(string route, object body, CancellationToken cancellationToken = default)
    {
        var response = await _client.PutAsJsonAsync(route, body, _jsonOptions, cancellationToken);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResponse?> PutRequestAsync<TResponse>(
        string route,
        object body,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        var response = await _client.PutAsJsonAsync(route, body, _jsonOptions, cancellationToken);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken);

        return await response.Content.ReadFromJsonAsync<TResponse>(_jsonOptions, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteRequestAsync(string route, CancellationToken cancellationToken = default)
    {
        var response = await _client.DeleteAsync(route, cancellationToken);
        await EnsureSuccessStatusCodeAsync(response, cancellationToken);
    }

    private Task EnsureSuccessStatusCodeAsync(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(response.EnsureSuccessStatusCode());
    }
}
