using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Mappings;

/// <summary>
/// Расширения для преобразования объектов.
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразует перечисляемый источник в список с пагинацией.
    /// </summary>
    /// <typeparam name="TDestination">Тип объекта пагинации.</typeparam>
    /// <param name="queryable">Перечисляемый источник.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список с пагинацией.</returns>
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(
        this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken)
        where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize, cancellationToken);

    /// <summary>
    /// Преобразует перечисляемый источник в список.
    /// </summary>
    /// <typeparam name="TDestination">Тип объекта пагинации.</typeparam>
    /// <param name="queryable">Перечисляемый источник.</param>
    /// <param name="configuration">Конфигурация преобразования объектов.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список.</returns>
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
        this IQueryable queryable, IConfigurationProvider configuration, CancellationToken cancellationToken)
        where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync(cancellationToken);
}
