using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Database;

namespace Wait.Features.Users.CreateUser;

public record CreateUserRequest(string FirstName, string LastName, string Username, string Password, string Email);
public record CreateUserResponse(string status, string message);


internal sealed class CreateUserHandler(AppDbContext dbContext, Users user)
{
    public async Task<Users> CreateUserAsync(IPasswordHasher<Users> passwordHasher, CancellationToken ct)
    {
        if (user is null)
        {
            throw new InvalidOperationException("The User cannot found");
        }


        await dbContext.User.AddAsync(user, ct);
        await dbContext.SaveChangesAsync(ct);

        return user;
    }
}

public sealed class CreateUser : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {

        app.MapPost("/api/user", async (CreateUserHandler handler, CreateUserRequest userRequest) =>
        {
            var request = userRequest.ToRequest();

            return request;
        });
    }


}

