using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Data;
using Wait.Database;
using Wait.Entities;


namespace Wait.Repositories;


public sealed class UserRepositories(IDbContextFactory<AppDbContext> dbContextFactory) : IUserRepositories
{
    public async Task<bool> CreateUserAsync(Users users)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var createUser = await dbContext.Set<Users>().AddAsync(users);
        await dbContext.SaveChangesAsync();
        return createUser is not null;
    }

    public async Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        return await dbContext.User.ToListAsync(ct);
    }

    public async Task<Users?> GetUserByIdAsync(Guid id)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        return await dbContext.User.FindAsync(id);

    }

    public async Task<bool> UpdateUserAsync(Users users, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        dbContext.Entry(users).CurrentValues.SetValues(users);

        await dbContext.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteUserAsync(Users users)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var deleteUser = dbContext.Set<Users>().Remove(users);
        await dbContext.SaveChangesAsync();
        return deleteUser is not null;


    }

}