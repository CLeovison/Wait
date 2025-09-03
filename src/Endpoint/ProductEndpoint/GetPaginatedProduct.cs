using Wait.Abstract;

namespace Wait.Endpoint.ProductEndpoint;

public sealed class GetPaginatedProduct : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/products/paginated", async (string? searchTerm,
            int page,
            int pageSize,
            string? sortBy,
            string? sortDirection) =>
        {

        });
    }
}