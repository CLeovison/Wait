namespace Wait.Domain.Entities;


public class RefreshToken
{
    public Guid TokenId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresOnUtc { get; set; }
    public bool RevokedToken { get; set; }
    public Guid UserId { get; set; }
    public Users? User { get; set; }
}