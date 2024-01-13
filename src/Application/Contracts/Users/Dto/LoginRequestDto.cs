namespace Application.Contracts.Users.Dto;

/// <summary>
/// Запрос на логин.
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Логин пользователя.
    /// </summary>
    public required string Login { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Чекбокс "Запомнить меня".
    /// </summary>
    public bool RememberMe { get; set; }
}
