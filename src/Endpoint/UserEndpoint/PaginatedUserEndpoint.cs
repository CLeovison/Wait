using Wait.Abstract;
using Wait.Contracts.Response.UserResponse;
using Wait.UserServices.Services;
using Microsoft.AspNetCore.Identity;
using Wait.Entities;
using Wait.Contracts.Data;


namespace Wait.Endpoint.UserEndpoint;

public sealed class PaginatedUserAsync(IUserServices userServices) : IEndpoint
{

    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/paginateduser", async () =>
        {

        });
    }
}