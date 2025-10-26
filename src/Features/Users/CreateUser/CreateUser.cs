using Wait.Abstract;
using Wait.Database;
using Wait.Domain.Entities;
using Wait.Entities;

namespace Wait.Features.Users.CreateUser;

public record CreateUserRequest(string FirstName, string LastName, string Username, string Password, string ConfirmPassowrd, string Email);
public record CreateUserResponse(string status, string message);


internal sealed class CreateUserHandler(AppDbContext dbContext, User user) 
{
    
}

public sealed class CreateUser : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {

        app.MapPost("/api/user", async (CreateUserHandler handler) =>
        {

        });
    }


}

