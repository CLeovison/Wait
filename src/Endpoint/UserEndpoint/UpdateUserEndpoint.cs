using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Contracts.Data;

using Wait.Services.UserServices;

using Wait.Domain.Entities;
using Wait.Infrastracture.Mapping;

namespace Wait.Endpoint.UserEndpoint;

public sealed class UpdateUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id}", async (Guid id, UserDto users, IPasswordHasher<Users> passwordHasher, CancellationToken ct) =>
        {
            var result = await userServices.UpdateUserAsync(id, users, ct);

            return result;
        });
    }
}