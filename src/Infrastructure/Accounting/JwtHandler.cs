using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Accounting;
using Application.Common.Accounting.Dto;
using Domain.Enums;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Accounting;

/// <summary>
/// Обработчик Jwt-токенов.
/// </summary>
public class JwtHandler : IJwtHandler
{
    private readonly JwtOptions _options;
    private readonly TimeProvider _timeProvider;

    /// <summary>
    /// Конструктор, подтягивающий зависимости через DI.
    /// </summary>
    /// <param name="options">Опции для JWT.</param>
    /// <param name="timeProvider">Сервис для работы со временем.</param>
    public JwtHandler(IOptions<JwtOptions> options, TimeProvider timeProvider)
    {
        _options = options.Value;
        _timeProvider = timeProvider;
    }

    /// <inheritdoc/>
    public string GenerateJwt(UserDto userDto, TimeSpan tokenLifeTime)
    {
        var claimsPrincipal = GetPrincipal(userDto);

        var dateTimeNow = _timeProvider.GetUtcNow().UtcDateTime;

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            notBefore: dateTimeNow,
            claims: claimsPrincipal.Claims,
            expires: dateTimeNow.Add(tokenLifeTime),
            signingCredentials: new SigningCredentials(
                GetSymmetricSecurityKey(_options.Key),
                SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

    /// <inheritdoc/>
    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    /// <inheritdoc/>
    public ClaimsPrincipal GetPrincipal(UserDto userDto)
    {
        var claims = new List<Claim>
        {
            new(ConstsClaims.ID, userDto.Id.ToString()),
            new(ConstsClaims.FIRST_NAME, userDto.FirstName),
            new(ConstsClaims.LAST_NAME, userDto.LastName),
            new(ConstsClaims.ROLE, userDto.Role.ToString()),
            new(ConstsClaims.EMAIL, userDto.Email)
        };

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "Token"));
    }

    /// <inheritdoc/>
    public UserDto DecodeToken(string userToken)
    {
        var claims = ValidateToken(userToken, _options.Key).Claims;

        return new UserDto
        {
            Id = long.TryParse(claims.First(x => x.Type == ConstsClaims.ID).Value, out var userId)
                ? userId
                : default,
            FirstName = claims.First(x => x.Type == ConstsClaims.FIRST_NAME).Value,
            LastName = claims.First(x => x.Type == ConstsClaims.LAST_NAME).Value,
            Role = Enum.TryParse<RoleType>(claims.First(x => x.Type == ConstsClaims.ROLE).Value, out var role)
                ? role
                : RoleType.None,
            Email = claims.First(x => x.Type == ConstsClaims.EMAIL).Value
        };
    }

    private JwtSecurityToken ValidateToken(string token, string jwtKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtKey);
        tokenHandler.ValidateToken(token,
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            }, out SecurityToken validatedToken);

        return (JwtSecurityToken) validatedToken;
    }

    private SymmetricSecurityKey GetSymmetricSecurityKey(string key)
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}
