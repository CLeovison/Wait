
using Wait.Abstract;
using Wait.Contracts.Request.ProductRequest;
using Wait.Infrastructure.Mapping;
using Wait.Services.ProductServices;

namespace Wait.Endpoint.ProductEndpoint;

public class CreateProductEndpoint : IEndpoint
{

    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (IProductService productService, IFormFile file, CreateProductRequest req, CancellationToken ct) =>
        {
            try
            {
                var productDto = req.ToCreateRequest();
                var productRequest = await productService.CreateProductAsync(productDto, file, ct);
                return Results.Created($"/products/{productRequest.ProductName}", productRequest);
            }
            catch (Exception)
            {
                throw;
            }
        })
        .DisableAntiforgery()
        .RequireAuthorization();
    }
}