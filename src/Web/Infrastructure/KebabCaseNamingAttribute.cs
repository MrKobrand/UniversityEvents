using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Web.Infrastructure;

/// <summary>
/// Атрибут приведения маршрутов к kebab-case.
/// </summary>
public class KebabCaseNamingAttribute : Attribute, IControllerModelConvention
{
    /// <summary>
    /// Применить атрибут к контроллеру.
    /// </summary>
    /// <param name="controller">Модель для конфигурирования контроллера.</param>
    public void Apply(ControllerModel controller)
    {
        controller.ControllerName = ToKebabCase(controller.ControllerName);

        foreach (var action in controller.Actions)
        {
            action.ActionName = ToKebabCase(action.ActionName);
        }
    }

    /// <summary>
    /// Приводит строку к kebab-case.
    /// </summary>
    /// <param name="input">Строка.</param>
    /// <returns>Строка в kebab-case.</returns>
    private static string ToKebabCase(string input)
    {
        var sb = new StringBuilder(input.Length * 2);

        foreach (var ch in input)
        {
            if (char.IsUpper(ch) && sb.Length > 0)
            {
                sb.Append('-');
            }

            sb.Append(char.ToLower(ch));
        }

        return sb.ToString();
    }
}
