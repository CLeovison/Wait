using Wait.Abstract;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Request.UserRequest;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;

public sealed class PaginatedUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/paginated", async (IUserServices userServices, [AsParameters] FilterUserRequest filters,
            string? searchTerm,
            int page,
            int pageSize,
            string? sortBy,
            string? sortDirection,
            CancellationToken ct) =>
        {
            var request = new PaginatedRequest
            {
                Page = page,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                SortBy = sortBy,
                SortDirection = sortDirection
            };


            var filterRequest = new FilterUserRequest
            {
                FirstName = filters.FirstName,
                LastName = filters.LastName,
                CreatedAt = filters.CreatedAt
            };
            return await userServices.PaginatedUsersAsync(request, filterRequest, ct);
        });
    }
}
