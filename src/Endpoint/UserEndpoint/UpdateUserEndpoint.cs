using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Entities;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;


public class UpdateUserEndpoint : IEndpoint
{

    private readonly IUserServices _userServices;
    public UpdateUserEndpoint(IUserServices userServices)
    {
        _userServices = userServices;
    }
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/update/{id}", async (Guid id, Users users) =>
        {
            await _userServices.UpdateUserAsync(id, users);
        });
    }
}