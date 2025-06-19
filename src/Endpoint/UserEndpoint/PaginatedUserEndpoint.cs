using Microsoft.AspNetCore.Mvc;
using Wait.Abstract;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Request.UserRequest;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;


public sealed class PaginatedUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/paginated", async (string? searchTerm,
            int page,
            int pageSize,
            string sortBy,
            string sortDirection,
         [FromBody] FilterUserRequest filter, CancellationToken ct) =>
        {


            var filteredUsers = new PaginatedRequest
            {
                Page = page,
                PageSize = pageSize,
                SearchTerm = searchTerm,
                SortBy = sortBy,
                SortDirection = sortDirection
            };
            return await userServices.PaginatedUserAsync(filteredUsers, filter, ct);
        });
    }
}