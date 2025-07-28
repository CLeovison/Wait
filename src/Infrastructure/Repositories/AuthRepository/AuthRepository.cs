using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Response;
using Wait.Database;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories;


public sealed class AuthRepostiory(AppDbContext dbContext) : IAuthRepository
{
    public async Task<RefreshToken?> GetRefreshTokenByUserId(Guid id, CancellationToken ct)
    {
        return await dbContext.RefreshToken.FirstOrDefaultAsync(x => x.UserId == id, ct);
    }
    public async Task<RefreshToken> SaveRefreshToken(RefreshToken refreshToken, CancellationToken ct)
    {
        var token = await dbContext.RefreshToken.AddAsync(refreshToken);
        await dbContext.SaveChangesAsync(ct);

        return token.Entity;
    }


}