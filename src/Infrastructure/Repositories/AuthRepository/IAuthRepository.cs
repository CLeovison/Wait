using Wait.Contracts.Response;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories;

public interface IAuthRepository
{
    Task<RefreshToken?> GetRefreshTokenByUserIdAsync(Guid id, CancellationToken ct);
    Task<RefreshToken> SaveRefreshTokenAsync(RefreshToken refreshToken, CancellationToken ct);
    Task<RefreshTokenResponse> RefreshTokenRotationAsync(string refreshToken, CancellationToken ct);
    Task RefreshTokenUpdate(RefreshToken refreshToken, CancellationToken ct);
    Task<bool> RevokeRefreshTokenAsync(Guid id, CancellationToken ct);
}