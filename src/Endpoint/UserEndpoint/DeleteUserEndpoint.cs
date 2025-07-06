using Wait.Abstract;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;

public sealed class DeleteUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/users/{id}", async (Guid id, IUserServices userServices, CancellationToken ct) =>
        {
            var removed = await userServices.DeleteUserAsync(id, ct);

            if (!removed)
                return Results.NotFound(new { message = "User not found or could not be deleted." });

            return Results.Ok(new { message = "The user was successfully deleted." });
        });
    }
}