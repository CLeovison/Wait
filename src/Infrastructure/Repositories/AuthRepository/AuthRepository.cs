using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Response;
using Wait.Database;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories;

public sealed class AuthRepostiory(AppDbContext dbContext) : IAuthRepository
{
    public async Task<RefreshToken?> GetRefreshTokenByUserIdAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.RefreshToken.FirstOrDefaultAsync(x => x.UserId == id, ct);
    }
    public async Task<RefreshToken> SaveRefreshTokenAsync(RefreshToken refreshToken, CancellationToken ct)
    {
        var token = await dbContext.RefreshToken.AddAsync(refreshToken);
        await dbContext.SaveChangesAsync(ct);
        return token.Entity;
    }
    public async Task<RefreshTokenResponse> RefreshTokenRotationAsync(string refreshToken)
    {
        var token = await dbContext.RefreshToken
        .Include(x => x.User)
        .FirstOrDefaultAsync(x => x.Token == refreshToken);

        return new RefreshTokenResponse { Token = token };
    }
    public async Task RefreshTokenUpdate(RefreshToken refreshToken)
    {
        dbContext.RefreshToken.Update(refreshToken);
        await dbContext.SaveChangesAsync();
    }
    public async Task<bool> RevokeRefreshTokenAsync(Guid id, CancellationToken ct)
    {
        await dbContext.RefreshToken.Where(x => x.UserId == id).ExecuteDeleteAsync();
        return true;
    }

}