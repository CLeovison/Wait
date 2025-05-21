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

    public async Task<Users?> UpdateUserAsync(Guid id, Users users, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var updateUser = await dbContext.User.FindAsync(users.UserId);

        if (updateUser is null)
        {
            return null;
        }
        updateUser.Username = users.Username;
        updateUser.Password = users.Password;
        updateUser.FirstName = users.FirstName;
        updateUser.Password = users.Password;
        updateUser.Email = users.Email;

        await dbContext.SaveChangesAsync(ct);
        return updateUser;



    }
    public async Task<bool> DeleteUserAsync(Users users)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var deleteUser = dbContext.Set<Users>().Remove(users);
        await dbContext.SaveChangesAsync();
        return deleteUser is not null;


    }

}