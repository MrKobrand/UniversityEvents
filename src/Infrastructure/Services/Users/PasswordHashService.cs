using System;
using System.Linq;
using System.Security.Cryptography;
using Application.Contracts.Users;
using Application.Contracts.Users.Models;

namespace Infrastructure.Services.Users;

/// <summary>
/// Сервис для работы с хэшированными паролями.
/// </summary>
public class PasswordHashService : IPasswordHashService
{
    private const short SIZE = 150;

    /// <inheritdoc/>
    public HashedPassword Hash(string password)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, SIZE);

        return new HashedPassword
        {
            Salt = Convert.ToBase64String(deriveBytes.Salt),
            Hash = Convert.ToBase64String(deriveBytes.GetBytes(SIZE))
        };
    }

    /// <inheritdoc/>
    public bool Verify(HashedPassword hashedPassword, string password)
    {
        var salt = Convert.FromBase64String(hashedPassword.Salt);
        var hash = Convert.FromBase64String(hashedPassword.Hash);

        using var deriveBytes = new Rfc2898DeriveBytes(password, salt);
        var newKey = deriveBytes.GetBytes(SIZE);

        return newKey.SequenceEqual(hash);
    }
}
