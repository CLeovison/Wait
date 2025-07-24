namespace Wait.Contracts.Data;


public class UserDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? ConfirmPassword { get; set; }
    public string Email { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

}