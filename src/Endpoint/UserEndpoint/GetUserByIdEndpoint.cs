using Wait.Abstract;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;

public sealed class GetUserByIdEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/{id}", async (Guid id, IUserServices userServices, CancellationToken ct) =>
        {
            try
            {
                var user = await userServices.GetUserByIdAsync(id, ct);

                if (user is null)
                {
                    return Results.NotFound(new { message = $"User with id {id} was not found." });
                }

                return Results.Ok(user);
            }
            catch (Exception ex)
            {

                return Results.Problem(
                    detail: ex.Message,
                    statusCode: 500,
                    title: "An error occurred while retrieving the user"
                );
            }
        })
        .RequireAuthorization();
    }
}