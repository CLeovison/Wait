namespace Wait.Contracts.Request.Common;


public sealed class PaginatedRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public bool SortDirection { get; set; } = false;
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage => Page > 1;
}