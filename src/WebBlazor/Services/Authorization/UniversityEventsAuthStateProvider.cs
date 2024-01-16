using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using WebBlazor.Contracts.Authorization;
using WebBlazor.Contracts.LocalStorage;
using WebBlazor.Contracts.Navigation;

namespace WebBlazor.Services.Authorization;

/// <summary>
/// Провайдер авторизованного состояния.
/// </summary>
public class UniversityEventsAuthStateProvider : AuthenticationStateProvider
{
    private static TaskCompletionSource<AuthenticationState>? Tcs;
    private static AuthenticationState State = new(new ClaimsPrincipal());

    private readonly IUniversityEventsNavigationManager _universityEventsNavigationManager;
    private readonly IAuthorizationService _authorizationService;
    private readonly HttpClient _httpClient;
    private readonly IUniversityEventsLocalStorageService _localStorage;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="universityEventsNavigationManager">Менеджер навигации по ключевым страницам UniversityEvents.</param>
    /// <param name="authorizationService">Сервис авторизации.</param>
    /// <param name="httpClient">Http-клиент.</param>
    /// <param name="localStorage">Предоставляет доступ к LocalStorage браузера.</param>
    public UniversityEventsAuthStateProvider(
        IUniversityEventsNavigationManager universityEventsNavigationManager,
        IAuthorizationService authorizationService,
        HttpClient httpClient,
        IUniversityEventsLocalStorageService localStorage)
    {
        _universityEventsNavigationManager = universityEventsNavigationManager;
        _authorizationService = authorizationService;
        _httpClient = httpClient;
        _localStorage = localStorage;
    }

    /// <inheritdoc/>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var tcs = Tcs;

        if (tcs is not null)
        {
            return await tcs.Task;
        }

        Tcs = new();

        var tokensPair = await _localStorage.GetTokensAsync();

        if (string.IsNullOrEmpty(tokensPair.AccessToken))
        {
            if (string.IsNullOrEmpty(tokensPair.RefreshToken))
            {
                State = new(new ClaimsPrincipal());
                _universityEventsNavigationManager.NavigateToLoginPage();
            }
            else
            {
                State = await RefreshTokensAsync(tokensPair.RefreshToken);
            }
        }
        else if (!IsValidToken(tokensPair.AccessToken))
        {
            State = await RefreshTokensAsync(tokensPair.RefreshToken);
        }
        else if (!State.User.Claims.Any())
        {
            State = GetAuthenticationState(tokensPair.AccessToken);
            await SetAuthorizationAsync(tokensPair.AccessToken);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(State));

        Tcs?.SetResult(State);
        Tcs = null;

        return State;
    }

    private async Task<AuthenticationState> RefreshTokensAsync(string refreshToken)
    {
        var state = new AuthenticationState(new ClaimsPrincipal());
        try
        {
            var tokensPair = await _authorizationService.RefreshTokensAsync(refreshToken);

            if (tokensPair is not null)
            {
                await _localStorage.SetTokensAsync(tokensPair);
                state = GetAuthenticationState(tokensPair.AccessToken);
                await SetAuthorizationAsync(tokensPair.AccessToken);
            }
        }
        catch
        {
            _universityEventsNavigationManager.NavigateToLoginPage();
        }

        return state;
    }

    private async Task SetAuthorizationAsync(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", accessToken);

        var authorizedDto = await _authorizationService.GetAuthorizedAsync();
        await _localStorage.SetUserAsync(authorizedDto!);
    }

    private static bool IsValidToken(string token)
    {
        var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
        var expireClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "exp");

        if (long.TryParse(expireClaim?.Value, out var expireSeconds))
        {
            var expireDateTime = DateTimeOffset.FromUnixTimeSeconds(expireSeconds);
            return DateTimeOffset.UtcNow < expireDateTime;
        }

        return false;
    }

    private static AuthenticationState GetAuthenticationState(string accessToken)
    {
        var identity = new ClaimsIdentity(
            new[]
            {
                new Claim("auth_token", accessToken)
            },
            "Jwt");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }
}
