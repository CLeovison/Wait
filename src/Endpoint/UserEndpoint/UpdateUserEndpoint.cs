using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Contracts.Request.UserRequest;
using Wait.Entities;
using Wait.Mapping;
using Wait.UserServices.Services;

namespace Wait.Endpoint.UserEndpoint;

public sealed class UpdateUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id}", async (Guid id, UpdateUserRequest req, IPasswordHasher<Users> passwordHaher, CancellationToken ct) =>
        {

            var updatedData = req.ToUpdate(passwordHaher);
            var updatedUserData = updatedData.ToDto(passwordHaher);
            var result = await userServices.UpdateUserAsync(updatedUserData, passwordHaher, ct);

            if (result)
            {
                return Results.Ok(new { message = "User Updated Successfully" });
            }
            else
            {
                return Results.BadRequest(new { message = "Failed to update user or user not found" });
            }
        });
    }
}