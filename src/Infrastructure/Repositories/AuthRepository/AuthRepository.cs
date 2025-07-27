using Wait.Database;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories;


public sealed class AuthRepostiory(AppDbContext dbContext) : IAuthRepository
{
    public async Task<RefreshToken> GenerateRefreshToken(RefreshToken refreshToken, CancellationToken ct)
    {
        var token = await dbContext.RefreshTokens.AddAsync(refreshToken);
        await dbContext.SaveChangesAsync(ct);

        return token.Entity;
    }
}