namespace Wait.Contracts.Request.Common;


public sealed class PaginatedRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; }
    public bool SortDirection { get; set; }
    public bool HasNextPage => Page > PageSize;
    public bool HasPreviousPage => Page < 1;
}