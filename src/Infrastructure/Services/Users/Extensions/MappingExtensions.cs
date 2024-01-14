using System.Collections.Generic;
using System.Linq;
using Application.Contracts.Users.Dto;
using Domain.Entities;

namespace Infrastructure.Services.Users.Extensions;

/// <summary>
/// Класс, содержащий расширения для преобразования типа
/// пользователя <see cref="User"/> в производные DTO.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразует сущность типа <see cref="User"/> в <see cref="DetailedUserDto"/>.
    /// </summary>
    /// <param name="value">Пользователь.</param>
    /// <returns>Детализированная информация о пользователе.</returns>
    public static DetailedUserDto ToDetailedDto(this User value)
    {
        return new DetailedUserDto
        {
            Id = value.Id,
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
            Role = value.Role,
            Email = value.Email,
            AvatarId = value.AvatarId,
            AvatarLink = value.AvatarImage?.Link
        };
    }

    /// <summary>
    /// Преобразует список сущностей типа <see cref="User"/> в <see cref="DetailedUserDto"/>.
    /// </summary>
    /// <param name="values">Пользователи.</param>
    /// <returns>Детализированная информация о пользователе.</returns>
    public static List<DetailedUserDto> ToDetailedDto(this List<User>? values)
    {
        return values is null
            ? new List<DetailedUserDto>()
            : values.Select(x => x.ToDetailedDto()).ToList();
    }

    /// <summary>
    /// Преобразует сущность типа <see cref="User"/> в <see cref="UserDto"/>.
    /// </summary>
    /// <param name="value">Пользователь.</param>
    /// <returns>Основная информация о пользователе.</returns>
    public static UserDto ToDto(this User value)
    {
        return new UserDto
        {
            Id = value.Id,
            FirstName = value.FirstName,
            LastName = value.LastName,
            MiddleName = value.MiddleName,
            Role = value.Role,
            Email = value.Email,
            AvatarId = value.AvatarId
        };
    }
}
