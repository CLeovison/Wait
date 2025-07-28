using Wait.Abstract;

namespace Wait.Endpoint.AuthEndpoint;


public sealed class RefreshtTokenEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/users/refresh-token", async () =>
        {

        });
    }
}