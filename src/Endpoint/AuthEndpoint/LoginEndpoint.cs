using Wait.Abstract;

namespace Wait.Endpoint.AuthEndpoint;


public sealed class LoginEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/login", async () =>
        {

        });
    }
}