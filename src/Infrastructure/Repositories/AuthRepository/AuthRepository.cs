using Microsoft.EntityFrameworkCore;
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
    public async Task<RefreshToken?> RefreshTokenRotationAsync(string refreshToken, CancellationToken ct)
    {
        return await dbContext.RefreshToken.Include(x => x.User).FirstOrDefaultAsync(x => x.Token == refreshToken);
    }

}