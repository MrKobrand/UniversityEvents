using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Users;
using Application.Contracts.Users.Dto;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.Users;
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
}
