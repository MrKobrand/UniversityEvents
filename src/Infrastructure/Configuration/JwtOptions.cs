using System;

namespace Infrastructure.Configuration;

/// <summary>
/// Опции для JWT.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Приложение, из которого отправляется токен.
    /// </summary>
    public required string Issuer { get; set; }

    /// <summary>
    /// Аудитория, получатели токена.
    /// </summary>
    public required string Audience { get; set; }

    /// <summary>
    /// Ключ кодирования.
    /// </summary>
    public required string Key { get; set; }

    /// <summary>
    /// Время жизни JWT токена.
    /// </summary>
    public TimeSpan LifeTime { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// Время жизни Refresh токена.
    /// </summary>
    public TimeSpan RefreshTokenLifeTime { get; set; } = TimeSpan.FromHours(18);

    /// <summary>
    /// Время жизни Refresh токена при включенной опции "Запомнить меня".
    /// </summary>
    public TimeSpan RememberRefreshTokenLifeTime { get; set; } = TimeSpan.FromDays(3600);
}
