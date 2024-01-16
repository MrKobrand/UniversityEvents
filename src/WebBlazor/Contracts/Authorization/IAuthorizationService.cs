using System.Threading.Tasks;
using WebBlazor.Contracts.Shared.Dto;

namespace WebBlazor.Contracts.Authorization;

/// <summary>
/// Сервис авторизации.
/// </summary>
public interface IAuthorizationService
{
    /// <summary>
    /// Получает авторизованного пользователя.
    /// </summary>
    /// <returns>Информация об авторизованном пользователе.</returns>
    Task<AuthUserDto?> GetAuthorizedAsync();

    /// <summary>
    /// Обновляет Refresh токены.
    /// </summary>
    /// <param name="refreshToken">Новый Refresh токен.</param>
    /// <returns>Пара JWT + RT токенов.</returns>
    Task<TokensPairDto?> RefreshTokensAsync(string refreshToken);
}
