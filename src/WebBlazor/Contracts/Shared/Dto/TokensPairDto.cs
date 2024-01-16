namespace WebBlazor.Contracts.Shared.Dto;

/// <summary>
/// Пара JWT + RT токенов.
/// </summary>
public class TokensPairDto
{
    /// <summary>
    /// Короткоживущий токен доступа.
    /// </summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// Долгоживущий токен доступа.
    /// </summary>
    public required string RefreshToken { get; set; }
}
