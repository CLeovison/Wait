using Wait.Abstract;
using Wait.UserServices.Services;

namespace Wait.Endpoint.UserEndpoint;


public sealed class DeleteUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/users/{id}", async (Guid id, CancellationToken ct) =>
        {
            var removeUser = await userServices.DeleteUserAsync(id, ct);

            return Results.Ok(new { message = "The User is Successfully Deleted" });
        });
    }
}