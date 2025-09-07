using Wait.Contracts.Request.Common;

namespace Wait.Infrastructure.Mapping;

/// <summary>
/// Provides mapping functionality to create instances of <c>PaginatedRequest</c>
/// from pagination and sorting parameters.
/// </summary>
public static class PaginationMapper
{
    /// <summary>
    /// Maps pagination and sorting parameters into a <c>PaginatedRequest</c> model.
    /// </summary>
    /// <param name="searchTerm">Optional search term used to filter the results.</param>
    /// <param name="page">The current page number.</param>
    /// <param name="pageSize">Number of items per page.</param>
    /// <param name="sortBy">Optional field name for sorting.</param>
    /// <param name="sortDirection">Sort direction (e.g., "asc" or "desc").</param>
    /// <returns>A <c>PaginatedRequest</c> object populated with the provided parameters.</returns>
    public static PaginatedRequest ToPaginate(string searchTerm, int page, int pageSize, string? sortBy, string? sortDirection)
    {
        return new PaginatedRequest
        {
            Page = page,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            SortBy = sortBy,
            SortDirection = sortDirection
        };
    }
}
