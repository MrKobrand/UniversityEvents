using System.ComponentModel;

namespace Web.Contracts.Users;

/// <summary>
/// Параметры запроса на логин.
/// </summary>
public class LoginCommand
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
    [DefaultValue(false)]
    public bool RememberMe { get; set; }
}
