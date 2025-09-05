using Wait.Abstract;
using Wait.Contracts.Data;

namespace Wait.Features.Users.Create;


public record CreateUserRequest(string FirstName, string LastName, string Username, string Password, string ConfirmPassowrd, string Email);
public record CreateUserResponse(string status, string message, List<UserDto> data);
public sealed class CreateUser : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {

    }
}