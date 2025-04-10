using Wait.Enums;

namespace Wait.Entities;

/// <summary>
/// This Class Is The Entity Based Class of the User
/// </summary>
public sealed class Users
{
    public Guid UserId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }

    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

}