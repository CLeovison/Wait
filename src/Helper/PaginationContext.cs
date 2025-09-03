using Wait.Contracts.Request.Common;

namespace Wait.Helper;

public sealed class PaginationContext
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int Skip => (Page - 1) * PageSize;
    public int Take => PageSize;
    public string? SortBy { get; init; }
    public string? DefaultSort { get; init; }
    public bool Descending { get; init; }

     public string EffectiveSortBy => 
         !string.IsNullOrWhiteSpace(SortBy) ? SortBy 
         : !string.IsNullOrWhiteSpace(DefaultSort) ? DefaultSort 
         : string.Empty;

}

public static class PaginationProcessor
{
    public static PaginationContext Create(PaginatedRequest req, string defaultSort)
    {
        var page = req.Page < 1 ? 1 : req.Page;
        var pageSize = req.PageSize is < 1 or > 100 ? 10 : req.PageSize;


        var descending = string.Equals(req.SortDirection, "desc", StringComparison.OrdinalIgnoreCase);

        return new PaginationContext
        {
            Page = page,
            PageSize = pageSize,
            SortBy = req.SortBy?.Trim(),
            DefaultSort = defaultSort,
            Descending = descending
        };
    }
}