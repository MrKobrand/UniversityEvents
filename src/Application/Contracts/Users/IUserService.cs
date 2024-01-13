using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Users.Dto;

namespace Application.Contracts.Users;

/// <summary>
/// Сервис для работы с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Предоставляет доступ пользователю к системе.
    /// </summary>
    /// <param name="request">Запрос на логин.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пара JWT + RT токенов.</returns>
    Task<TokensPairDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);
}
