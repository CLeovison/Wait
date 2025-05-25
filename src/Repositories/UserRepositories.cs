using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Data;
using Wait.Contracts.Request.UserRequest;
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
    public async Task<Users> PaginatedUserAsync(Users users, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var paginatedUserQuery = dbContext.User.OrderBy(x => x.CreatedAt);

    }

    public async Task<Users?> UpdateUserAsync(Guid id, Users users, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var existingUser = await dbContext.User.FindAsync(id);

        if (existingUser is null)
        {
            return null;
        }
        existingUser.FirstName = users.FirstName;
        existingUser.LastName = users.LastName;
        existingUser.Username = users.Username;
        existingUser.Password = users.Password;
        existingUser.Email = users.Email;

        dbContext.Entry(users).CurrentValues.SetValues(users);

        await dbContext.SaveChangesAsync(ct);

        return existingUser;
    }
    public async Task<bool> DeleteUserAsync(Users users)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var deleteUser = dbContext.Set<Users>().Remove(users);
        await dbContext.SaveChangesAsync();
        return deleteUser is not null;


    }

}