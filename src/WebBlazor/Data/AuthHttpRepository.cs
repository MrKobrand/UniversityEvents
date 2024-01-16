using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace WebBlazor.Data;

/// <summary>
/// Http-репозиторий с авторизованным доступом.
/// </summary>
public class AuthHttpRepository : HttpRepository, IAuthHttpRepository
{
    private readonly AuthenticationStateProvider _stateProvider;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="client">Http-клиент.</param>
    /// <param name="stateProvider">Провайдер авторизованного состояния.</param>
    public AuthHttpRepository(HttpClient client, AuthenticationStateProvider stateProvider)
        : base(client)
    {
        _stateProvider = stateProvider;
    }

    /// <inheritdoc/>
    public override async Task DeleteRequestAsync(string route, CancellationToken cancellationToken = default)
    {
        await _stateProvider.GetAuthenticationStateAsync();
        await base.DeleteRequestAsync(route, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<Stream> GetFileRequestAsync(
        string route,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        await _stateProvider.GetAuthenticationStateAsync();
        return await base.GetFileRequestAsync(route, queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<T?> GetRequestAsync<T>(
        string route,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
        where T : class
    {
        await _stateProvider.GetAuthenticationStateAsync();
        return await base.GetRequestAsync<T>(route, queryParams, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task PatchRequestAsync(string route, object body, CancellationToken cancellationToken = default)
    {
        await _stateProvider.GetAuthenticationStateAsync();
        await base.PatchRequestAsync(route, body, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<TResponse?> PatchRequestAsync<TResponse>(
        string route,
        object body,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        await _stateProvider.GetAuthenticationStateAsync();
        return await base.PatchRequestAsync<TResponse>(route, body, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task PostRequestAsync(string route, object body, CancellationToken cancellationToken = default)
    {
        await _stateProvider.GetAuthenticationStateAsync();
        await base.PostRequestAsync(route, body, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<TResponse?> PostRequestAsync<TResponse>(
        string route,
        object body,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        await _stateProvider.GetAuthenticationStateAsync();
        return await base.PostRequestAsync<TResponse>(route, body, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<TResponse?> PostRequestAsync<TResponse>(
        string route,
        HttpContent body,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        await _stateProvider.GetAuthenticationStateAsync();
        return await base.PostRequestAsync<TResponse>(route, body, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<string> PostRequestRawResultAsync(
        string route,
        object body,
        CancellationToken cancellationToken = default)
    {
        await _stateProvider.GetAuthenticationStateAsync();
        return await base.PostRequestRawResultAsync(route, body, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task PutRequestAsync(string route, object body, CancellationToken cancellationToken = default)
    {
        await _stateProvider.GetAuthenticationStateAsync();
        await base.PutRequestAsync(route, body, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<TResponse?> PutRequestAsync<TResponse>(
        string route,
        object body,
        CancellationToken cancellationToken = default)
        where TResponse : class
    {
        await _stateProvider.GetAuthenticationStateAsync();
        return await base.PutRequestAsync<TResponse>(route, body, cancellationToken);
    }
}
