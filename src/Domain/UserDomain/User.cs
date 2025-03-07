using Wait.Domain.UserDomain.Common;

namespace Wait.Domain.UserDomain;

/// <summary>
/// This Class Is The Entity Based Class of the User
/// </summary>
public class User
{
    public Guid UserId { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string FirstName { get; init; }
    public DateOnly Birthday { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public DateOnly CreatedAt { get; init; }
    public DateOnly UpdatedAt { get; init; }
}