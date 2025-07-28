using Wait.Contracts.Response;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories;


public interface IAuthRepository
{
    Task<RefreshToken?> GetRefreshTokenByUserIdAsync(Guid id, CancellationToken ct);
    Task<RefreshToken> SaveRefreshTokenAsync(RefreshToken refreshToken, CancellationToken ct);
    Task<RefreshToken?> RefreshTokenRotationAsync(string refreshToken, CancellationToken ct);
}