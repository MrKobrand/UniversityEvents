using System;
using Application.Common.Accounting;
using Application.Common.Accounting.Dto;
using Application.Common.Interfaces;
using Application.Contracts.Users;
using Application.Contracts.Users.Dto;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services.Users;

/// <summary>
/// Сервис для аутентификации пользователей.
/// </summary>
public class AuthService : IAuthService
{
    private const string REFRESH_TOKEN_COOKIE_NAME = "refresh_token";

    private readonly IHttpContextWrapper _httpContextWrapper;
    private readonly IJwtHandler _jwtHandler;
    private readonly JwtOptions _options;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="httpContextWrapper">Сервис для получения доступа к контексту http-запроса.</param>
    /// <param name="jwtHandler">Обработчик Jwt-токенов.</param>
    /// <param name="options">Опции для JWT.</param>
    public AuthService(
        IHttpContextWrapper httpContextWrapper,
        IJwtHandler jwtHandler,
        IOptions<JwtOptions> options)
    {
        _httpContextWrapper = httpContextWrapper;
        _jwtHandler = jwtHandler;
        _options = options.Value;
    }

    /// <inheritdoc/>
    public TokensPairDto SignIn(AuthUserDto userDto, TimeSpan tokenLifeTime)
    {
        var userToken = _jwtHandler.GenerateJwt(userDto, tokenLifeTime);
        var refreshToken = _jwtHandler.GenerateRefreshToken();

        var refreshTokenLifeTime = userDto.RememberMe
            ? _options.RememberRefreshTokenLifeTime
            : _options.RefreshTokenLifeTime;

        var refreshTokenOptions = new CookieOptions
        {
            Expires = new DateTimeOffset().Add(refreshTokenLifeTime),
            MaxAge = refreshTokenLifeTime
        };

        _httpContextWrapper.SignIn(REFRESH_TOKEN_COOKIE_NAME, refreshToken, refreshTokenOptions);

        return new TokensPairDto
        {
            AccessToken = userToken,
            RefreshToken = refreshToken,
        };
    }

    /// <inheritdoc/>
    public void SignOut()
    {
        _httpContextWrapper.SignOut(REFRESH_TOKEN_COOKIE_NAME);
    }
}
