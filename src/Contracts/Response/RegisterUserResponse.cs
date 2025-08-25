namespace Wait.Contracts.Response;

public sealed class RegisterUserResponse
{
    public Guid UserId { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
}