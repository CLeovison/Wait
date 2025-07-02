using Wait.Abstract;

using Wait.Contracts.Request.UserRequest;
using Wait.Services.UserServices;
using Wait.Infrastracture.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Domain.Entities;

namespace Wait.Endpoint.UserEndpoint;


public class CreateUserEndpoint(IUserServices userServices) : IEndpoint
{


    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/create", async (CreateUserRequest req, CancellationToken ct) =>
        {
            var userDto = req.ToRequest();

            var userCreated = await userServices.CreateUserAsync(userDto, ct);
            return Results.Ok(userCreated);
        });
    }
}