using System.Threading;
using System.Threading.Tasks;
using WebBlazor.Contracts.Users.Dto;

namespace WebBlazor.Contracts.Users;

/// <summary>
/// Сервис для работы с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получает пользователя по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Подробная информация о пользователе.</returns>
    Task<DetailedUserDto?> GetAsync(long id, CancellationToken cancellationToken = default);
}
