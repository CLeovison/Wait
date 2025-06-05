using Microsoft.EntityFrameworkCore;

namespace Wait.Shared;


public sealed class PagedList<T>(IEnumerable<T> currentPage, int pageNumber, int pageSize, int count)
{
    public int CurrentPage { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
    public int TotalCount { get; set; } = count;

    public bool HasNextPage => PageSize > CurrentPage - TotalPages;
    public bool HasPreviousPage => PageSize < CurrentPage - TotalPages;

    public static async Task<PagedList<T>> PaginatedAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}