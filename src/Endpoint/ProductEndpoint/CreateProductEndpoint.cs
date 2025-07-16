using Wait.Abstract;
using Wait.Contracts.Request.ProductRequest;
using Wait.Infrastructure.Mapping;
using Wait.Services.ProductServices;

namespace Wait.Endpoint.ProductEndpoint;

public class CreateProductEndpoint : IEndpoint
{

    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (IProductService productService, CreateProductRequest req, CancellationToken ct) =>
        {
            var productDto = req.ToCreateRequest();
            var productRequest = await productService.CreateProductAsync(productDto, ct);
            return productRequest;
        });
    }
}