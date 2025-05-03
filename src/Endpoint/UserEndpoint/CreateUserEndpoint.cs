using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Contracts.Request.UserRequest;
using Wait.Entities;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;


public class CreateUserEndpoint : IEndpoint
{
    private readonly IUserServices _userServices;
    public CreateUserEndpoint(IUserServices userServices)
    {
        _userServices = userServices;
    }
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/create", async (Users users, CreateUserRequest request, IPasswordHasher<Users> passwordHasher) =>
        {
            await _userServices.CreateUserAsync(users, passwordHasher, request);
        });
    }
}