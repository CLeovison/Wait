namespace Wait.Contracts.Response.UserResponse;


public sealed class UserResponse
{

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateOnly Birthday { get; set; } = default!;
}