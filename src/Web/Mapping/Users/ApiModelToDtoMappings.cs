using Application.Contracts.Users.Dto;
using Web.Contracts.Users;

namespace Web.Mapping.Users;

/// <summary>
/// Класс, содержащий расширения для преобразования моделей API запросов в DTO.
/// </summary>
public static class ApiModelToDtoMappings
{
    /// <summary>
    /// Преобразует запрос на логин в DTO.
    /// </summary>
    /// <param name="command">Запрос на логин.</param>
    /// <returns>DTO запроса на логин.</returns>
    public static LoginRequestDto ToRequestDto(this LoginCommand command)
    {
        return new LoginRequestDto
        {
            Login = command.Login,
            Password = command.Password,
            RememberMe = command.RememberMe,
        };
    }

    /// <summary>
    /// Преобразует запрос на создание пользователя в DTO.
    /// </summary>
    /// <param name="command">Запрос на создание пользователя.</param>
    /// <returns>DTO запроса на создание пользователя.</returns>
    public static CreateUserRequestDto ToRequestDto(this CreateUserCommand command)
    {
        return new CreateUserRequestDto
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            MiddleName = command.MiddleName,
            Role = command.Role,
            Email = command.Email,
            Password = command.Password,
            AvatarId = command.AvatarId
        };
    }
}
