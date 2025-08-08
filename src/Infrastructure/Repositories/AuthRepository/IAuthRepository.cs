using Wait.Contracts.Request.AuthRequest;
using Wait.Contracts.Response;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories;

public interface IAuthRepository
{
    Task<RefreshToken?> GetRefreshTokenByUserIdAsync(Guid id, CancellationToken ct);
    Task<RefreshToken> SaveRefreshTokenAsync(RefreshToken refreshToken, CancellationToken ct);
    Task<RefreshToken> UserTokenInfo();
    Task<RefreshTokenResponse> RefreshTokenRotationAsync(string refreshToken);
    Task RefreshTokenUpdate(RefreshToken refreshToken);
    Task<bool> RevokeRefreshTokenAsync(Guid id, CancellationToken ct);

    //ForgotPassword
    //ResetPassword
    //Email Verification
}