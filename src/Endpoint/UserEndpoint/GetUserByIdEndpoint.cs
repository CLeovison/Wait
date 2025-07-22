using Wait.Abstract;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;

public sealed class GetUserByIdEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/{id}", async (Guid id, IUserServices userServices, CancellationToken ct) =>
        {
            return await userServices.GetUserByIdAsync(id, ct);
        });
    }
}