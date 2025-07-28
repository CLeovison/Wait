using Wait.Contracts.Response;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories;


public interface IAuthRepository
{
    Task<RefreshToken?> GetRefreshTokenByUserId(Guid id, CancellationToken ct);
    Task<RefreshToken> SaveRefreshToken(RefreshToken refreshToken, CancellationToken ct);
} 