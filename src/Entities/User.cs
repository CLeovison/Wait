using Wait.Enums;

namespace Wait.Entities;

/// <summary>
/// This Class Is The Entity Based Class of the User
/// </summary>
public sealed class User
{
    public Guid UserId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }
    public required bool IsSoftDeleted { get; init; } = false;
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }

}