using Wait.Abstract;

namespace Wait.Features.Users.UpdateUser;


public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string Email);
    
public record UpdateUserResponse(
    string FirstName,
    string LastName,
    string Username,
    string Email,
    DateOnly UpdatedAt);


public sealed class UpdateUser : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id}", async () =>
        {

        });
    }
}