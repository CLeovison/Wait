using Wait.Contracts.Request.Common;

namespace Wait.Helper;

public sealed class PaginationContext
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int Skip => (Page - 1) * PageSize;
    public int Take => PageSize;
    public string SortBy { get; init; } = "FirstName";
    public bool Descending { get; init; }
}

public static class PaginationProcessor
{
    public static PaginationContext Create(PaginatedRequest req)
    {
        var page = req.Page < 1 ? 1 : req.Page;
        var pageSize = req.PageSize is < 1 or > 100 ? 10 : req.PageSize;

        var sortBy = string.IsNullOrWhiteSpace(req.SortBy) ? "FirstName" : req.SortBy.Trim();
        var descending = string.Equals(req.SortDirection, "desc", StringComparison.OrdinalIgnoreCase);

        return new PaginationContext
        {
            Page = page,
            PageSize = pageSize,
            SortBy = sortBy,
            Descending = descending
        };
    }
}