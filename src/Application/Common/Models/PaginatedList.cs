using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Models;

/// <summary>
/// Список с пагинацией.
/// </summary>
/// <typeparam name="T">Тип объекта пагинации.</typeparam>
public class PaginatedList<T>
{
    /// <summary>
    /// Список объектов.
    /// </summary>
    public IReadOnlyCollection<T> Items { get; }

    /// <summary>
    /// Номер страницы.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Общее количество страниц.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Общее количество объектов.
    /// </summary>
    public int TotalCount { get; }

    /// <summary>
    /// Конструктор, инициализирующий список с пагинацией.
    /// </summary>
    /// <param name="items">Список объектов.</param>
    /// <param name="count">Общее количество объектов.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        TotalCount = count;
        Items = items;
    }

    /// <summary>
    /// Имеет ли предыдущую страницу.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Имеет ли следующую страницу.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Создает список с пагинацией.
    /// </summary>
    /// <param name="source">Перечисляемый источник.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Список с пагинацией.</returns>
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();

        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}
