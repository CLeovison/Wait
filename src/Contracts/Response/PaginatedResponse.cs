

namespace Wait.Contracts.Response;

public class PaginatedResponse<T>(List<T> items, int page, int pageSize, int totalCount)
{
    public List<T> Items { get; } = items;

    public int Page { get; } = page;

    //PageSize = Number of Items Per Page
    public int PageSize { get; } = (int)Math.Ceiling((decimal)totalCount / (decimal)pageSize);

    public int TotalCount { get; } = totalCount;

    public bool HasNextPage => Page * PageSize < TotalCount;

    public bool HasPreviousPage => Page > 1;


}