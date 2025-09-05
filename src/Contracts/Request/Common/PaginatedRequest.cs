namespace Wait.Contracts.Request.Common;


public sealed class PaginatedRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SearchTerm { get; set; } = string.Empty;
    public string? SortBy { get; set; }
    public string? SortDirection { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage => Page > 1;
}