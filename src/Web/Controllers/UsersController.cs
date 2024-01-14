using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Users;
using Application.Contracts.Users.Dto;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.Users;
using Web.Infrastructure;
using Web.Mapping.Users;

namespace Web.Controllers;

/// <summary>
/// Контроллер пользователей.
/// </summary>
public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Входит в систему с выдачей JWT + RT токенов.
    /// </summary>
    /// <param name="command">Параметры запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пара JWT + RT токенов.</returns>
    [HttpPost("[action]")]
    public Task<TokensPairDto> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        return _userService.LoginAsync(command.ToRequestDto(), cancellationToken);
    }

    /// <summary>
    /// Получает пользователя по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о пользователе.</returns>
    [HttpGet("{id:long}")]
    public Task<DetailedUserDto?> GetById([FromRoute] long id, CancellationToken cancellationToken)
    {
        return _userService.GetAsync(id, cancellationToken);
    }

    /// <summary>
    /// Получает список пользователей.
    /// </summary>
    /// <param name="limit">Лимит сущностей.</param>
    /// <param name="search">Строка поиска.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список сущностей с информацией о пользователях.</returns>
    [HttpGet]
    public Task<List<DetailedUserDto>> GetList(
        [FromQuery] int? limit,
        [FromQuery] string? search,
        CancellationToken cancellationToken)
    {
        return _userService.GetListAsync(limit, search, cancellationToken);
    }

    /// <summary>
    /// Создает пользователя.
    /// </summary>
    /// <param name="command">Параметры на создание пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о пользователе.</returns>
    [HttpPost]
    [Authorization(RoleType.Administrator)]
    public Task<UserDto> Create(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        return _userService.CreateAsync(command.ToRequestDto(), cancellationToken);
    }

    /// <summary>
    /// Удаляет пользователя.
    /// </summary>
    /// <param name="id">Уникальный идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Информация о пользователе.</returns>
    [HttpDelete("{id:long}")]
    [Authorization(RoleType.Administrator)]
    public Task<UserDto?> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        return _userService.DeleteAsync(id, cancellationToken);
    }
}
