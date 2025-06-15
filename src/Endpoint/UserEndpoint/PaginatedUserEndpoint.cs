using Microsoft.AspNetCore.Mvc;
using Wait.Abstract;
using Wait.Contracts.Request.Common;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;


public sealed class PaginatedUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/paginated", async ([AsParameters] PaginatedRequest req, CancellationToken ct) =>
        {

            return await userServices.PaginatedUserAsync(req, ct);
        });
    }
}