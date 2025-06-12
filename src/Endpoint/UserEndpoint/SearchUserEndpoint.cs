using Wait.Abstract;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;


public sealed class SearchUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/search", async (string? searchTerm, CancellationToken ct) =>
        {
            return await userServices.SearchUserAsync(searchTerm, ct);
        });
    }
}