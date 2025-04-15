using Wait.Abstract;
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
        app.MapPost("/api/create", async (Users users, CancellationToken ct) =>
        {
            await _userServices.CreateUserAsync(users);
        });
    }
}