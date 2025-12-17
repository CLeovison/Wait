
using Microsoft.AspNetCore.Mvc;
using Wait.Abstract;
using Wait.Contracts.Request.ProductRequest;
using Wait.Infrastructure.Mapping;
using Wait.Services.ProductServices;

namespace Wait.Endpoint.ProductEndpoint;

public class CreateProductEndpoint : IEndpoint
{

    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (
    IProductService productService,
    [FromBody] CreateProductRequest req,
    CancellationToken ct) =>
        {
            try
            {
                var productDto = req.ToCreateRequest();
                var productRequest = await productService.CreateProductAsync(productDto, ct);
                return Results.Created($"/products/{productRequest.ProductName}", productRequest);
            }
            catch (Exception)
            {
                throw;
            }
        });
    }
}