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
}
