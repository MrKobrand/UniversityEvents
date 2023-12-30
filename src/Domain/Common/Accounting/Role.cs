using Domain.Enums;

namespace Domain.Common.Accounting;

/// <summary>
/// Модель роли пользователя.
/// </summary>
public class Role
{
    private readonly RoleType _roleType;

    private Role(RoleType roleType)
    {
        _roleType = roleType;
    }

    /// <summary>
    /// Роль пользователя.
    /// </summary>
    public static Role User { get; } = new(RoleType.User);

    /// <summary>
    /// Роль администратора.
    /// </summary>
    public static Role Administrator { get; } = new(RoleType.Administrator);

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        return obj is Role role && Equals(role);
    }

    /// <summary>
    /// Сравнивает роли.
    /// </summary>
    /// <param name="other">Роль для сравнения.</param>
    public bool Equals(Role other)
    {
        return other._roleType == _roleType;
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return _roleType.GetHashCode();
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return _roleType.ToString();
    }

    /// <inheritdoc />
    public static bool operator ==(Role l, Role r)
    {
        return l.Equals(r);
    }

    /// <inheritdoc />
    public static bool operator !=(Role l, Role r)
    {
        return !l.Equals(r);
    }

    /// <inheritdoc />
    public static bool operator <(Role l, Role r)
    {
        if (l is null || r is null)
        {
            return false;
        }

        return GetRoleLevel(l) < GetRoleLevel(r);
    }

    /// <inheritdoc />
    public static bool operator >(Role l, Role r)
    {
        if (l is null || r is null)
        {
            return false;
        }

        return GetRoleLevel(l) > GetRoleLevel(r);
    }

    /// <inheritdoc />
    public static bool operator <=(Role l, Role r)
    {
        if (l is null || r is null)
        {
            return false;
        }

        return GetRoleLevel(l) <= GetRoleLevel(r);
    }

    /// <inheritdoc />
    public static bool operator >=(Role l, Role r)
    {
        if (l is null || r is null)
        {
            return false;
        }

        return GetRoleLevel(l) >= GetRoleLevel(r);
    }

    /// <inheritdoc />
    public static implicit operator RoleType(Role r)
    {
        return r._roleType;
    }

    /// <inheritdoc />
    public static implicit operator int(Role r)
    {
        return (int) r._roleType;
    }

    /// <inheritdoc />
    public static implicit operator Role(RoleType r)
    {
        return new Role(r);
    }

    /// <summary>
    /// Получает уровень роли.
    /// </summary>
    /// <param name="role">Роль.</param>
    /// <returns>Числовое представление уровня роли.</returns>
    private static int GetRoleLevel(Role role)
    {
        return GetRoleLevel(role._roleType);
    }

    /// <summary>
    /// Получает уровень роли.
    /// </summary>
    /// <param name="role">Тип роли.</param>
    /// <returns>Числовое представление уровня роли.</returns>
    private static int GetRoleLevel(RoleType role)
    {
        return role switch
        {
            RoleType.User => 10,
            RoleType.Administrator => 20,
            _ => 0
        };
    }
}
