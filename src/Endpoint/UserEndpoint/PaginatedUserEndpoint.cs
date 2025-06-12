using Wait.Abstract;
using Wait.Contracts.Request.Common;
using Wait.UserServices.Services;

namespace Wait.Endpoint.UserEndpoint;


public sealed class PaginatedUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/paginated/{page}", async (string searchTerm, int page, int pageSize, string sortBy, CancellationToken ct) =>
        {

            var paginatedRequest = new PaginatedRequest
            {
                Page = page,
                PageSize = pageSize,

            };

            var paginatedUser = await userServices.PaginatedUserAsync(paginatedRequest, ct);
        });
    }
}