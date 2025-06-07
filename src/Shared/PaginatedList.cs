using Microsoft.EntityFrameworkCore;

namespace Wait.Shared;

public class PaginatedList<T>(List<T> items, int page, int pageSize, int totalCount)
{
    public List<T> Items { get; } = items;
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;

    public static async Task<PaginatedList<T>> GetPaginatedAsync(IQueryable<T> query, int page, int pageSize)
    {
        int totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, page, pageSize, totalCount);
    }
}