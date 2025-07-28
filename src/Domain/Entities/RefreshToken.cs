namespace Wait.Domain.Entities;


public class RefreshToken
{
    public Guid TokenId { get; set; }
    public string Token { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public Users? User { get; set; }
}