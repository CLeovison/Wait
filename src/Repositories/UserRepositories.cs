using Microsoft.EntityFrameworkCore;
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

    public async Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        return await dbContext.User.FindAsync(id, ct);

    }
    public async Task<List<Users>> PaginatedUserAsync(int limit, int page)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        IQueryable<Users> userQuery = dbContext.User.AsQueryable();

        var paginatedUser = await userQuery.OrderBy(x => x.CreatedAt)
             .ThenBy(x => x.FirstName)
             .Skip((page - 1) * limit)
             .Take(limit)
             .AsNoTracking()
             .ToListAsync();

        return paginatedUser;

    }

    public async Task<Users?> UpdateUserAsync(Users users, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        dbContext.Entry(users).CurrentValues.SetValues(users);

        await dbContext.SaveChangesAsync(ct);

        return users;
    }
    public async Task<bool> DeleteUserAsync(Users users)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var deleteUser = dbContext.Set<Users>().Remove(users);
        await dbContext.SaveChangesAsync();

        return deleteUser is not null;

    }

}