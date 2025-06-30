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
        app.MapPost("/api/create", async (CreateUserRequest req, IPasswordHasher<Users> passwordHasher, CancellationToken ct) =>
        {
            var userDto = req.ToRequest(passwordHasher);

            var userCreated = await userServices.CreateUserAsync(userDto, ct);
            return Results.Ok(userCreated);
        });
    }
}