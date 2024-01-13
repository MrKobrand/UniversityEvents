using Application.Common.Accounting.Dto;

namespace Application.Common.Accounting;

/// <summary>
/// Предоставляет доступ к данным пользователя при запросе.
/// </summary>
public interface IUserContextAccessor
{
    /// <summary>
    /// Устанавливает текущего пользователя.
    /// </summary>
    /// <param name="userDto">Информация о пользователе.</param>
    void SetCurrentUser(UserDto userDto);

    /// <summary>
    /// Проверяет, установлен ли текущий пользователь.
    /// </summary>
    /// <returns>
    /// <see langword="true"/>, если текущий пользователь не установлен, иначе - <see langword="false"/>.
    /// </returns>
    bool IsNotSet();
}
