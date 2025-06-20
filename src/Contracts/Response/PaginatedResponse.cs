namespace Wait.Contracts.Response;

public class PaginatedResponse<T>(List<T> items, int page, int pageSize, int totalCount)
{
    public List<T> Items { get; } = items;

    public int Page { get; } = page;

    //PageSize = Number of Items Per Page
    public int PageSize { get; } = pageSize;

    public int TotalCount { get; } = totalCount;
    public int TotalPages => (int)Math.Ceiling((decimal)totalCount / pageSize);

    public bool HasNextPage => Page < TotalPages;

    public bool HasPreviousPage => Page > 1;


}