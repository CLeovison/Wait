using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Contracts.Data;
using Wait.Services.UserServices;
using Wait.Domain.Entities;


namespace Wait.Endpoint.UserEndpoint;

public sealed class UpdateUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id}", async (Guid id, IUserServices userServices, UserDto users, IPasswordHasher<Users> passwordHasher, CancellationToken ct) =>
        {

            var result = await userServices.UpdateUserAsync(id, users, ct);

            return result;
        });
    }
}