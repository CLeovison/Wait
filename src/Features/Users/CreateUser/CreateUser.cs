using Wait.Abstract;
using Wait.Contracts.Data;
using Wait.Database;

namespace Wait.Features.Users.CreateUser;

public record CreateUserRequest(string FirstName, string LastName, string Username, string Password, string ConfirmPassowrd, string Email);
public record CreateUserResponse(string status, string message, List<UserDto> data);


internal sealed class CreateUserHandler(AppDbContext dbContext, Users users)
{
    var request = dbContext.User.Add(users);
}

public sealed class CreateUser : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/user", Handler);
    }


}

