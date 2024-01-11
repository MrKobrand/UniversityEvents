using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Services;

/// <summary>
/// Поддерживает Restful API, обрабатывая возвращаемые результаты запросов.
/// </summary>
public class SupportRestfulApi : IResultFilter
{
    private static readonly Lazy<SupportRestfulApi> _inst = new(() => new SupportRestfulApi());

    /// <summary>
    /// Единственный экземпляр класса <see cref="SupportRestfulApi"/>.
    /// </summary>
    public static SupportRestfulApi Instance => _inst.Value;

    /// <inheritdoc/>
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }

    /// <inheritdoc/>
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
        {
            var returnType = actionDescriptor.MethodInfo.ReturnType;

            if (returnType == typeof(void) || returnType == typeof(Task))
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NoContent;
                return;
            }

            if (context.Result is ObjectResult { Value: null })
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }
        }
    }
}
