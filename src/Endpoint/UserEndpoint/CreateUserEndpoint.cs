using Wait.Abstract;

using Wait.Contracts.Request.UserRequest;
using Wait.UserServices.Services;
using Wait.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Entities;

namespace Wait.Endpoint.UserEndpoint;


public class CreateUserEndpoint(IUserServices userServices) : IEndpoint
{


    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/create", async (CreateUserRequest req, IPasswordHasher<Users> passwordHasher) =>
        {
            var userDto = req.ToRequest(passwordHasher);

            var userCreated = await userServices.CreateUserAsync(userDto);
            return Results.Ok(userCreated);
        });
    }
}