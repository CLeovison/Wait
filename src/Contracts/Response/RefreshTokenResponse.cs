using Wait.Domain.Entities;

namespace Wait.Contracts.Response;


public sealed class RefreshTokenResponse
{
    public RefreshToken? Token { get; set; }
    public bool IsExpired => Token?.ExpiresAt < DateTime.UtcNow;

}