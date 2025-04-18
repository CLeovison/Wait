
using Wait.Abstract;
using Wait.Entities;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;

public class DeleteUserEndpoint : IEndpoint
{
    private readonly IUserServices _userServices;
    public DeleteUserEndpoint(IUserServices userServices)
    {
        _userServices = userServices;
    }
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/{id}", async (Guid id) =>
        {
            var deleteUser = await _userServices.DeleteUserAsync(id);

            return deleteUser;
        });
    }
}