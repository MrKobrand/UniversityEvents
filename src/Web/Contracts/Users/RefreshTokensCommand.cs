namespace Web.Contracts.Users;

/// <summary>
/// Параметры запроса на обновление токенов.
/// </summary>
public class RefreshTokensCommand
{
    /// <summary>
    /// Refresh токен.
    /// </summary>
    public required string RefreshToken { get; set; }
}
