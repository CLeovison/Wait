namespace Wait.Contracts.Data;


public class UserDto
{
    public Guid UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public required string Email { get; set; }
    public DateOnly Birthday { get; set; }
    public DateOnly CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

}