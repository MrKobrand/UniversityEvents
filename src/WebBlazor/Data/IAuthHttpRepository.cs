using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebBlazor.Data;

/// <summary>
/// Http-репозиторий с авторизованным доступом.
/// </summary>
public interface IAuthHttpRepository
{
    /// <summary>
    /// Получает содержимое файла в виде потока.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="queryParams">Query-параметры.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Содержимое файла.</returns>
    Task<Stream> GetFileRequestAsync(
        string route,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает запрошенную сущность.
    /// </summary>
    /// <typeparam name="T">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="queryParams">Query-параметры.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность заданного типа.</returns>
    Task<T?> GetRequestAsync<T>(
        string route,
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    /// Отправляет запрос на создание сущности.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task PostRequestAsync(string route, object body, CancellationToken cancellationToken = default);

    /// <summary>
    /// Отправляет запрос на создание сущности.
    /// </summary>
    /// <typeparam name="TResponse">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность, преобразованная в указанный тип.</returns>
    Task<TResponse?> PostRequestAsync<TResponse>(string route, object body, CancellationToken cancellationToken = default)
        where TResponse : class;

    /// <summary>
    /// Отправляет запрос на создание сущности.
    /// </summary>
    /// <typeparam name="TResponse">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса в виде Http содержимого.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность, преобразованная в указанный тип.</returns>
    Task<TResponse?> PostRequestAsync<TResponse>(string route, HttpContent body, CancellationToken cancellationToken = default)
        where TResponse : class;

    /// <summary>
    /// Отправляет запрос на создание объекта с получением ответа в виде строки.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Строковый ответ.</returns>
    Task<string> PostRequestRawResultAsync(string route, object body, CancellationToken cancellationToken = default);

    /// <summary>
    /// Отправляет запрос на обновление сущности.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task PutRequestAsync(string route, object body, CancellationToken cancellationToken = default);

    /// <summary>
    /// Отправляет запрос на обновление сущности.
    /// </summary>
    /// <typeparam name="TResponse">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность, преобразованная в указанный тип.</returns>
    Task<TResponse?> PutRequestAsync<TResponse>(string route, object body, CancellationToken cancellationToken = default)
        where TResponse : class;

    /// <summary>
    /// Отправляет запрос на обновление части сущности.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task PatchRequestAsync(string route, object body, CancellationToken cancellationToken = default);

    /// <summary>
    /// Отправляет запрос на обновление части сущности.
    /// </summary>
    /// <typeparam name="TResponse">Тип, в который преобразовывается ответ.</typeparam>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="body">Параметры запроса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность, преобразованная в указанный тип.</returns>
    Task<TResponse?> PatchRequestAsync<TResponse>(string route, object body, CancellationToken cancellationToken = default)
        where TResponse : class;

    /// <summary>
    /// Отправляет запрос на удаление.
    /// </summary>
    /// <param name="route">Путь к ресурсу.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteRequestAsync(string route, CancellationToken cancellationToken = default);
}
