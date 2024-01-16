using System.Threading.Tasks;
using WebBlazor.Contracts.Authorization;
using WebBlazor.Contracts.Shared.Dto;
using WebBlazor.Data;

namespace WebBlazor.Services.Authorization;

/// <summary>
/// Сервис авторизации.
/// </summary>
public class AuthorizationService : IAuthorizationService
{
    private readonly IHttpRepository _httpRepository;

    private const string AUTHORIZED = "users/authorized";
    private const string REFRESH_TOKENS = "users/refresh-tokens";

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="httpRepository">Http-репозиторий.</param>
    public AuthorizationService(IHttpRepository httpRepository)
    {
        _httpRepository = httpRepository;
    }

    /// <inheritdoc/>
    public Task<AuthUserDto?> GetAuthorizedAsync()
    {
        return _httpRepository.GetRequestAsync<AuthUserDto>(AUTHORIZED);
    }

    /// <inheritdoc/>
    public Task<TokensPairDto?> RefreshTokensAsync(string refreshToken)
    {
        return _httpRepository.PostRequestAsync<TokensPairDto>(REFRESH_TOKENS, new
        {
            RefreshToken = refreshToken
        });
    }
}
