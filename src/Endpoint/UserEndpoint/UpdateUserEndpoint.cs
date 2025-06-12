using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Contracts.Data;

using Wait.Domain.Entities;
using Wait.Services.UserServices;
using Wait.Mapping;

namespace Wait.Endpoint.UserEndpoint;

public sealed class UpdateUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id}", async (Guid id, UserDto users, IPasswordHasher<Users> passwordHasher, CancellationToken ct) =>
        {
            var updatedUser = users.ToEntities(passwordHasher);


            var result = await userServices.UpdateUserAsync(id, updatedUser, ct);

            return result;
        });
    }
}