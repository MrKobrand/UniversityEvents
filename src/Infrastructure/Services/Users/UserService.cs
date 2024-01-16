using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Accounting;
using Application.Common.Accounting.Dto;
using Application.Common.Files;
using Application.Common.Interfaces;
using Application.Contracts.Users;
using Application.Contracts.Users.Dto;
using Application.Contracts.Users.Models;
using Domain.Entities;
using Infrastructure.Configuration;
using Infrastructure.Exceptions;
using Infrastructure.Services.EventCategories.Extensions;
using Infrastructure.Services.Users.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Users;

/// <summary>
/// Сервис для работы с пользователями.
/// </summary>
public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUniversityEventsDbContext _dbContext;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IAuthService _authService;
    private readonly IFileStorage _fileStorage;
    private readonly IUserContext _userContext;
    private readonly TimeProvider _timeProvider;
    private readonly JwtOptions _options;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="logger">Логгер событий.</param>
    /// <param name="dbContext">Контекст базы данных.</param>
    /// <param name="passwordHashService">Сервис для работы с хэшированными паролями.</param>
    /// <param name="authService">Сервис для аутентификации пользователей.</param>
    /// <param name="fileStorage">Сервис для работы с файловым хранилищем.</param>
    /// <param name="userContext">Авторизационные данные пользователя.</param>
    /// <param name="options">Опции для JWT.</param>
    /// <param name="timeProvider">Сервис для работы со временем.</param>
    public UserService(
        ILogger<UserService> logger,
        IUniversityEventsDbContext dbContext,
        IPasswordHashService passwordHashService,
        IAuthService authService,
        IFileStorage fileStorage,
        IUserContext userContext,
        IOptions<JwtOptions> options,
        TimeProvider timeProvider)
    {
        _logger = logger;
        _dbContext = dbContext;
        _passwordHashService = passwordHashService;
        _authService = authService;
        _fileStorage = fileStorage;
        _userContext = userContext;
        _timeProvider = timeProvider;
        _options = options.Value;
    }

    /// <inheritdoc/>
    public AuthorizedUserDto GetAuthorizedUser()
    {
        return new AuthorizedUserDto
        {
            Id = _userContext.Id,
            FirstName = _userContext.FirstName,
            LastName = _userContext.LastName,
            Role = _userContext.Role,
            Email = _userContext.Email
        };
    }

    /// <inheritdoc/>
    public async Task<TokensPairDto> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<LoginAsync>: {@Request}", request);

        var user = await GetUserAsync(request.Login, request.Password, cancellationToken);

        var userDto = new AuthUserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            Email = user.Email,
            RememberMe = request.RememberMe
        };

        var tokensPair = _authService.SignIn(userDto, _options.LifeTime);

        var dateTimeNow = _timeProvider.GetUtcNow();

        var refreshToken = new RefreshToken
        {
            UserId = user.Id,
            Token = tokensPair.RefreshToken,
            RememberMe = request.RememberMe,
            ExpiryDate = request.RememberMe
                ? dateTimeNow.Add(_options.RememberRefreshTokenLifeTime)
                : dateTimeNow.Add(_options.RefreshTokenLifeTime)
        };

        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Refresh token for user with id {Id} created", refreshToken.UserId);

        return tokensPair;
    }

    /// <inheritdoc/>
    public void Logout()
    {
        _authService.SignOut();
    }

    /// <inheritdoc/>
    public async Task<TokensPairDto> RefreshTokensPairAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var refreshTokenFromDb = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken, cancellationToken);

        if (refreshTokenFromDb is null || refreshTokenFromDb.ExpiryDate < _timeProvider.GetUtcNow())
        {
            throw new UnauthorizedException();
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == refreshTokenFromDb.UserId, cancellationToken);

        var userDto = new AuthUserDto
        {
            Id = user!.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            Email = user.Email,
            RememberMe = refreshTokenFromDb.RememberMe
        };

        var tokensPair = _authService.SignIn(userDto, _options.LifeTime);
        refreshTokenFromDb.Token = tokensPair.RefreshToken;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return tokensPair;
    }

    /// <inheritdoc/>
    public async Task<DetailedUserDto?> GetAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetAsync>: {Id}", id);

        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return user?.ToDetailedDto();
    }

    /// <inheritdoc/>
    public async Task<List<DetailedUserDto>> GetListAsync(int? limit, string? search, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<GetListAsync>: {Limit}, {Search}", limit, search);

        var usersQuery = _dbContext.Users
            .AsNoTracking();

        if (!string.IsNullOrEmpty(search))
        {
            usersQuery = usersQuery.Where(x =>
                x.FirstName.Contains(search) ||
                x.LastName.Contains(search) ||
                (!string.IsNullOrEmpty(x.MiddleName) && x.MiddleName.Contains(search)));
        }

        var users = await usersQuery
            .Take(limit ?? 25)
            .ToListAsync(cancellationToken);

        return users.ToDetailedDto();
    }

    /// <inheritdoc/>
    public async Task<UserDto> CreateAsync(CreateUserRequestDto request, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<CreateAsync>: {@Request}", request);

        using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        await _fileStorage.MoveAndSaveTempFileAsync(request.AvatarId, cancellationToken);

        var hashedPassword = _passwordHashService.Hash(request.Password);

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Role = request.Role,
            Email = request.Email,
            Password = hashedPassword.Hash,
            PasswordSalt = hashedPassword.Salt,
            AvatarId = request.AvatarId
        };

        var createdUser = await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        _logger.LogInformation("User with name {FirstName} {LastName} created", request.FirstName, request.LastName);

        return createdUser.Entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task<UserDto?> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("<DeleteAsync>: {Id}", id);

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user is not null)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            await _fileStorage.DeleteFileAsync(user.AvatarId, cancellationToken);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("User with {Id} found and deleted", id);
        }

        return user?.ToDto();
    }

    private async Task<User> GetUserAsync(string email, string password, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (user is null)
        {
            _logger.LogInformation("Incorrect login or password");
            throw new ArgumentException("Incorrect login or password");
        }

        var passwordVerified = CheckPassword(user.Password, user.PasswordSalt, password);

        if (!passwordVerified)
        {
            _logger.LogInformation("Incorrect login or password");
            throw new ArgumentException("Incorrect login or password");
        }

        return user;
    }

    /// <summary>
    /// Проверяет корректность пароля.
    /// </summary>
    /// <param name="hash">Хеш.</param>
    /// <param name="salt">Соль.</param>
    /// <param name="realPassword">Настоящий пароль.</param>
    /// <returns><see langword="true"/>, если пароль совпадает, иначе - <see langword="false"/>.</returns>
    private bool CheckPassword(string hash, string salt, string realPassword)
    {
        var hashedPassword = new HashedPassword
        {
            Hash = hash,
            Salt = salt
        };

        return _passwordHashService.Verify(hashedPassword, realPassword);
    }
}
