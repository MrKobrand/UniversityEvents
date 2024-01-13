using System;
using Application.Common.Accounting;
using Application.Common.Accounting.Dto;
using Domain.Enums;

namespace Infrastructure.Accounting;

/// <summary>
/// Предоставляет доступ к данным пользователя при запросе.
/// </summary>
public class UserContextAccessor : IUserContextAccessor, IUserContext
{
    private UserDto? _currentUser;

    /// <inheritdoc/>
    public long Id => _currentUser?.Id ?? default;

    /// <inheritdoc/>
    public string FirstName => _currentUser?.FirstName ?? string.Empty;

    /// <inheritdoc/>
    public string LastName => _currentUser?.LastName ?? string.Empty;

    /// <inheritdoc/>
    public RoleType Role => _currentUser?.Role ?? RoleType.None;

    /// <inheritdoc/>
    public string Email => _currentUser?.Email ?? string.Empty;

    /// <inheritdoc/>
    public string AccessToken => _currentUser?.AccessToken ?? string.Empty;

    /// <inheritdoc/>
    public string RefreshToken => _currentUser?.RefreshToken ?? string.Empty;

    /// <inheritdoc/>
    public bool RememberMe => _currentUser?.RememberMe ?? default;

    /// <inheritdoc/>
    public void SetCurrentUser(UserDto userDto)
    {
        if (_currentUser is null)
        {
            _currentUser = userDto;
        }
        else
        {
            throw new Exception("Авторизационные данные пользователя уже установлены.");
        }
    }

    /// <inheritdoc/>
    public bool IsNotSet()
    {
        return _currentUser is null;
    }
}
