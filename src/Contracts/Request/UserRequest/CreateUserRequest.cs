namespace Wait.Contracts.Request.UserRequest;

public class CreateUserRequest
{
    public Guid UserId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public DateOnly CreatedAt { get; init; }
}