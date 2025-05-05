namespace Wait.Contracts.Data;


public class UserDto
{
    public Guid UserId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Username { get; init; }

    public required string Email { get; init; }
    public DateOnly Birthday { get; init; }
    public DateOnly CreatedAt { get; init; }

}