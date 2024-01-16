using System.Threading.Tasks;
using Blazored.LocalStorage;
using WebBlazor.Contracts.LocalStorage;
using WebBlazor.Contracts.Shared.Dto;

namespace WebBlazor.Services.LocalStorage;

/// <summary>
/// Предоставляет доступ к LocalStorage браузера.
/// </summary>
public class UniversityEventsLocalStorageService : IUniversityEventsLocalStorageService
{
    private readonly ILocalStorageService _storageService;

    private const string AUTH_TOKEN = "auth_token";
    private const string USER_CONTEXT = "user_context";
    private const string REFRESH_TOKEN = "refresh_token";

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="storageService">LocalStorage браузера.</param>
    public UniversityEventsLocalStorageService(ILocalStorageService storageService)
    {
        _storageService = storageService;
    }

    /// <inheritdoc/>
    public async Task SetTokensAsync(TokensPairDto tokensPair)
    {
        await _storageService.SetItemAsStringAsync(AUTH_TOKEN, tokensPair.AccessToken);
        await _storageService.SetItemAsStringAsync(REFRESH_TOKEN, tokensPair.RefreshToken);
    }

    /// <inheritdoc/>
    public async Task<TokensPairDto> GetTokensAsync()
    {
        return new TokensPairDto
        {
            AccessToken = await _storageService.GetItemAsStringAsync(AUTH_TOKEN),
            RefreshToken = await _storageService.GetItemAsStringAsync(REFRESH_TOKEN)
        };
    }

    /// <inheritdoc/>
    public async Task RemoveTokensAsync()
    {
        await _storageService.RemoveItemAsync(AUTH_TOKEN);
        await _storageService.RemoveItemAsync(REFRESH_TOKEN);
    }

    /// <inheritdoc/>
    public async Task SetUserAsync(AuthUserDto authUserDto)
    {
        await _storageService.SetItemAsync(USER_CONTEXT, authUserDto);
    }
}
