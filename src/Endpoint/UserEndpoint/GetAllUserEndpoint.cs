using Wait.Abstract;
using Wait.UserServices.Services;

namespace Wait.Endpoint.UserEndpoint;


public sealed class GetAllUserEndpoint(IUserServices userServices) : IEndpoint
{

    public void Endpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("/api/users", async (CancellationToken ct) =>
        {
            return await userServices.GetAllUserAsync(ct);
        });
    }
}