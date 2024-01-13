using Application.Contracts.Users.Models;

namespace Application.Contracts.Users;

/// <summary>
/// Сервис для работы с хэшированными паролями.
/// </summary>
public interface IPasswordHashService
{
    /// <summary>
    /// Хэширует пароль.
    /// </summary>
    /// <param name="password">Пароль.</param>
    /// <returns>Хэшированный пароль.</returns>
    public HashedPassword Hash(string password);

    /// <summary>
    /// Проверяет на эквивалентность пароль и его хэшированную версию.
    /// </summary>
    /// <param name="hashedPassword">Хэшированный пароль.</param>
    /// <param name="password">Пароль.</param>
    /// <returns><see langword="true"/>, если пароль совпадает, иначе - <see langword="false"/>.</returns>
    public bool Verify(HashedPassword hashedPassword, string password);
}
