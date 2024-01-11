using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Domain.Common;

namespace Infrastructure.Services;

/// <summary>
/// Сервис для работы с перечислениями.
/// </summary>
public class EnumService : IEnumService
{
    /// <inheritdoc/>
    public List<EnumValueModel> GetEnumInfo<TEnum>()
        where TEnum : Enum
    {
        return Enum
            .GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(ps => new EnumValueModel
            {
                Id = (int) (object) ps,
                Key = ps.ToString()
            })
            .ToList();
    }
}
