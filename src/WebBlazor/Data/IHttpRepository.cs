using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebBlazor.Data;

/// <summary>
/// Http-репозиторий.
/// </summary>
public interface IHttpRepository
{
    /// <summary>
    /// Получает содержимое файла в виде потока.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="queryParams">Query-параметры.</param>
    /// <returns>Содержимое файла.</returns>
    Task<Stream> GetFileRequestAsync(string route, Dictionary<string, string>? queryParams = null);

    /// <summary>
    /// Получает запрошенную сущность.
    /// </summary>
    /// <typeparam name="T">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="queryParams">Query-параметры.</param>
    /// <returns>Сущность заданного типа.</returns>
    Task<T?> GetRequestAsync<T>(string route, Dictionary<string, string>? queryParams = null)
        where T : class;

    /// <summary>
    /// Отправляет запрос на создание сущности.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    Task PostRequestAsync(string route, object body);

    /// <summary>
    /// Отправляет запрос на создание сущности.
    /// </summary>
    /// <typeparam name="TResponse">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <returns>Сущность, преобразованная в указанный тип.</returns>
    Task<TResponse?> PostRequestAsync<TResponse>(string route, object body)
        where TResponse : class;

    /// <summary>
    /// Отправляет запрос на создание сущности.
    /// </summary>
    /// <typeparam name="TResponse">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса в виде Http содержимого.</param>
    /// <returns>Сущность, преобразованная в указанный тип.</returns>
    Task<TResponse?> PostRequestAsync<TResponse>(string route, HttpContent body)
        where TResponse : class;

    /// <summary>
    /// Отправляет запрос на создание объекта с получением ответа в виде строки.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <returns>Строковый ответ.</returns>
    Task<string> PostRequestRawResultAsync(string route, object body);

    /// <summary>
    /// Отправляет запрос на обновление сущности.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    Task PutRequestAsync(string route, object body);

    /// <summary>
    /// Отправляет запрос на обновление сущности.
    /// </summary>
    /// <typeparam name="TResponse">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <returns>Сущность, преобразованная в указанный тип.</returns>
    Task<TResponse?> PutRequestAsync<TResponse>(string route, object body)
        where TResponse : class;

    /// <summary>
    /// Отправляет запрос на обновление части сущности.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    Task PatchRequestAsync(string route, object body);

    /// <summary>
    /// Отправляет запрос на обновление части сущности.
    /// </summary>
    /// <typeparam name="TResponse">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <returns>Сущность, преобразованная в указанный тип.</returns>
    Task<TResponse?> PatchRequestAsync<TResponse>(string route, object body)
        where TResponse : class;

    /// <summary>
    /// Отправляет запрос на удаление.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    Task DeleteRequestAsync(string route);
}
