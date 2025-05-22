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
        app.MapPut("/api/users/{id}", async (Guid id, UpdateUserRequest req, IPasswordHasher<Users> passwordHasher, CancellationToken ct) =>
        {
            var updatedUser = req.ToRequestUpdate(passwordHasher);


            var result = await userServices.UpdateUserAsync(id, updatedUser, passwordHasher, ct);

            return result;
        });
    }
}