using Wait.Abstract;

namespace Wait.Endpoint.UserEndpoint;


public class CreateUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("", async () =>
        {

        });
    }
}