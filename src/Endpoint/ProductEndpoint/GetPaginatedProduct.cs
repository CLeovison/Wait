using Wait.Abstract;
using Wait.Contracts.Request.ProductRequest;
using Wait.Infrastructure.Mapping;
using Wait.Services.ProductServices;

namespace Wait.Endpoint.ProductEndpoint;

public sealed class GetPaginatedProduct : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/paginated", async (IProductService productService, [AsParameters] FilterProductRequest filters, string searchTerm,
            int page,
            int pageSize,
            string? sortBy,
            string? sortDirection,
            CancellationToken ct) =>
        {

            var request = PaginationMapper.ToPaginate(searchTerm, page, pageSize, sortBy, sortDirection);

            var filterRequest = new FilterProductRequest
            {
                ProductName = filters.ProductName,
                Size = filters.Size,
                Color = filters.Color
            };
            return await productService.GetPaginatedProductAsync(filterRequest,request,ct);
        });
    }
}