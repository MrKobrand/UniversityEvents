namespace Application.Contracts.Users.Dto;

/// <summary>
/// Параметры для создания пользователя.
/// </summary>
public class CreateUserRequestDto : UserDto
{
    /// <summary>
    /// Пароль.
    /// </summary>
    public required string Password { get; set; }
}
