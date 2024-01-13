namespace Application.Contracts.Users.Models;

/// <summary>
/// Пара хэш-соль.
/// </summary>
public class HashedPassword
{
    /// <summary>
    /// Хэш.
    /// </summary>
    public required string Hash { get; set; }

    /// <summary>
    /// Соль.
    /// </summary>
    public required string Salt { get; set; }
}
