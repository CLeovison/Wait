using Wait.Abstract;
using Wait.Services.ProductServices;

namespace Wait.Endpoint.ProductEndpoint;

public class CreateProductEndpoint : IEndpoint
{

    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (IProductService productService) =>
        {

        });
    }
}