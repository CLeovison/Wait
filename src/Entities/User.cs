using Wait.Enums;

namespace Wait.Entities;

/// <summary>
/// This Class Is The Entity Based Class of the User
/// </summary>
public sealed class User
{
    public Guid UserId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }
    public DateOnly CreatedAt { get; init; }

}