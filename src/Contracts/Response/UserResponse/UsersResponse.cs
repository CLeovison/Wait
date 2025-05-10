namespace Wait.Contracts.Response.UserResponse;


public sealed class UserResponse
{
    public Guid UserId { get; init; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Username { get; set; } = default!;

    public string Email { get; set; } = default!;
}