using Wait.Abstract;
using Wait.Services.UserServices;
using Wait.Contracts.Request.UserRequest;
using Wait.Infrastructure.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Contracts.Response.UserResponse;
using Wait.Extensions;


namespace Wait.Endpoint.UserEndpoint;

public sealed class UpdateUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id}", async (Guid id, UpdateUserRequest request, IUserServices userServices, IPasswordHasher<UserDto> passwordHasher, CancellationToken ct) =>
        {

            try
            {
                var updatedUser = request.ToRequestUpdate(passwordHasher);
                var result = await userServices.UpdateUserAsync(id, updatedUser, ct);
                return result is null ? Results.NotFound() : Results.Ok(result);

            }
            catch (Exception ex)
            {
                return Results.Problem(
                 detail: ex.Message,
                 statusCode: 500,
                 title: "An error occurred while deleting the user"
             );

            }
        })
        .WithValidation<UpdateUserRequest>();
    }
}
