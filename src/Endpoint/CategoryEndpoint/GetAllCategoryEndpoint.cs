using Wait.Abstract;
using Wait.Contracts.Request.CategoriesRequest;
using Wait.Infrastructure.Mapping;
using Wait.Services.Categories;

namespace Wait.Endpoint.CategoryEndpoint;

public sealed class GetAllCategoryEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/category/paginated", async (ICategoriesService categoriesService, [AsParameters] FilterCategoriesRequest filter,
            string? searchTerm,
            int page,
            int pageSize,
            string? sortBy,
            string? sortDirection,
            CancellationToken ct) =>
        {
            var request = PaginationMapper.ToPaginate(searchTerm!, page, pageSize, sortBy, sortDirection);
            var filters = new FilterCategoriesRequest
            {
                CategoryName = filter.CategoryName
            };
            return await categoriesService.GetAllCategoryAsync(request, filters, ct);
        });
    }
}
