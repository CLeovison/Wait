using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories;


public interface IAuthRepository
{
    Task<RefreshToken> GenerateRefreshToken(RefreshToken refreshToken, CancellationToken ct);
}