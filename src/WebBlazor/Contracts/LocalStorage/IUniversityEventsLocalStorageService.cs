using System.Threading.Tasks;
using WebBlazor.Contracts.Shared.Dto;

namespace WebBlazor.Contracts.LocalStorage;

/// <summary>
/// Предоставляет доступ к LocalStorage браузера.
/// </summary>
public interface IUniversityEventsLocalStorageService
{
    /// <summary>
    /// Устанавливает пару JWT + RT токенов.
    /// </summary>
    /// <param name="tokensPair">Пара JWT + RT токенов.</param>
    Task SetTokensAsync(TokensPairDto tokensPair);

    /// <summary>
    /// Получает пару JWT + RT токенов.
    /// </summary>
    /// <returns>Пара JWT + RT токенов.</returns>
    Task<TokensPairDto> GetTokensAsync();

    /// <summary>
    /// Удаляет пару JWT + RT токенов.
    /// </summary>
    Task RemoveTokensAsync();

    /// <summary>
    /// Устанавливает параметры пользователя.
    /// </summary>
    /// <param name="authUserDto">Информация об авторизованном пользователе.</param>
    Task SetUserAsync(AuthUserDto authUserDto);
}
