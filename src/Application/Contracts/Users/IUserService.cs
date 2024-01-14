using System.Collections.Generic;
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

    /// <summary>
    /// Получает пользователя по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Подробная информация о пользователе.</returns>
    Task<DetailedUserDto?> GetAsync(long id, CancellationToken cancellationToken);

    /// <summary>
    /// Получает список пользователей.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей.</returns>
    Task<List<DetailedUserDto>> GetListAsync(int? limit, string? search, CancellationToken cancellationToken);

    /// <summary>
    /// Создает пользователя.
    /// </summary>
    /// <param name="request">Запрос на создание пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о пользователе.</returns>
    Task<UserDto> CreateAsync(CreateUserRequestDto request, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Удаленный пользователь.</returns>
    Task<UserDto?> DeleteAsync(long id, CancellationToken cancellationToken);
}
