using System;
using Domain.Common;

namespace Domain.Entities;

/// <summary>
/// Refresh токен.
/// </summary>
public class RefreshToken : BaseEntity
{
    /// <summary>
    /// Уникальный идентификатор пользователя.
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Пользователь, владеющий токеном.
    /// </summary>
    public virtual User User { get; set; } = default!;

    /// <summary>
    /// Токен для обновления пары JWT + RefreshToken.
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Статус чекбокса "Запомнить меня".
    /// </summary>
    public bool RememberMe { get; set; }

    /// <summary>
    /// Срок действия токена.
    /// </summary>
    public DateTimeOffset ExpiryDate { get; set; }
}
