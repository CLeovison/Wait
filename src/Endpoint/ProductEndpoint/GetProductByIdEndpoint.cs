using Microsoft.AspNetCore.Mvc;
using Wait.Abstract;
using Wait.Services.ProductServices;

namespace Wait.Endpoint.ProductEndpoint;

public sealed class GetProductByIdEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/product/{id}", async (Guid id, IProductService productService, CancellationToken ct) =>
        {
            return await productService.GetProductByIdAsync(id, ct);
        });
    }
}