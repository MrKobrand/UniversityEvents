using System;
using System.Collections.Generic;
using Domain.Common;

namespace Application.Common.Interfaces;

/// <summary>
/// Сервис для работы с перечислениями.
/// </summary>
public interface IEnumService
{
    /// <summary>
    /// Получает состояния перечисления.
    /// </summary>
    /// <typeparam name="TEnum">Тип перечисления. Должен наследовать <see cref="Enum" />.</typeparam>
    /// <returns>Объект типа <see cref="EnumValueModel" />, описывающий состояния перечисления.</returns>
    IEnumerable<EnumValueModel> GetEnumInfo<TEnum>()
        where TEnum : Enum;
}
