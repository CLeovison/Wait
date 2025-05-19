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
        app.MapPut("/api/users/update/{id:guid}", async (Guid id, UpdateUserRequest req, IPasswordHasher<Users> passwordHasher, CancellationToken ct) =>
        {
            var toUpdate = req.ToRequestUpdate(passwordHasher);
            return await userServices.UpdateUserAsync(toUpdate, passwordHasher, ct);
        });
    }
}