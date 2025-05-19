using Wait.Abstract;
using Wait.UserServices.Services;

namespace Wait.Endpoint.UserEndpoint;


public sealed class DeleteUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/users/{id}", async (Guid id) =>
        {
            var removeUser = await userServices.DeleteUserAsync(id);

            return removeUser;
        });
    }
}