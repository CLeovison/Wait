using Wait.Abstract;

namespace Wait.Endpoint.ProductEndpoint;

public class CreateProductEndpoint : IEndpoint
{

    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("", () =>
        {

        });
    }
}