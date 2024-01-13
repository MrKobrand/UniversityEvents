using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Accounting;
using Domain.Common.Accounting;
using Domain.Enums;
using Infrastructure.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Infrastructure;

/// <summary>
/// Атрибут авторизации.
/// </summary>
public class AuthorizationAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly Role _requiredRole;

    /// <summary>
    /// Предоставляет доступ с ролью по-умолчанию.
    /// </summary>
    public AuthorizationAttribute()
    {
        _requiredRole = Role.User;
    }

    /// <summary>
    /// Предоставляет доступ с указанной ролью.
    /// </summary>
    /// <param name="requiredRole">Необходимая для доступа роль.</param>
    public AuthorizationAttribute(RoleType requiredRole)
    {
        _requiredRole = requiredRole;
    }

    /// <inheritdoc />
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userToken = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(userToken))
        {
            context.Result = new UnauthorizedResult();
            return Task.CompletedTask;
        }

        var userContext = context.HttpContext.RequestServices.GetRequiredService<IUserContextAccessor>();
        var userDtoRole = Role.User;

        if (userContext.IsNotSet())
        {
            try
            {
                var jwtHandler = context.HttpContext.RequestServices.GetRequiredService<IJwtHandler>();
                var userDto = jwtHandler.DecodeToken(userToken);
                userContext.SetCurrentUser(userDto);
                context.HttpContext.User = jwtHandler.GetPrincipal(userDto);
                userDtoRole = (Role) userDto.Role;
            }
            catch
            {
                context.Result = new UnauthorizedResult();
                return Task.CompletedTask;
            }
        }

        if (_requiredRole > userDtoRole)
        {
            context.Result = ForbiddenResult.Instance;
        }

        return Task.CompletedTask;
    }
}
