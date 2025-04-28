using Microsoft.EntityFrameworkCore;
using Wait.Database;
using Wait.Entities;

namespace Wait.Repositories;


public class UserRepositories : IUserRepositories
{
    private readonly IDbContextFactory<AppDbContext> _dbContext;

    public UserRepositories(IDbContextFactory<AppDbContext> dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> CreateUserAsync(Users user)
    {
        using var dbContext = _dbContext.CreateDbContext();
        var createUser = await dbContext.Set<Users>().AddAsync(user);
        await dbContext.SaveChangesAsync();

        return createUser is null;
    }
    public async Task<IEnumerable<Users>> GetAllUsersAsync(CancellationToken cancellationToken)

    {
        using var dbContext = _dbContext.CreateDbContext();
        return await dbContext.User.ToListAsync(cancellationToken);
    }

    public async Task<Users?> GetUsersByIdAsync(Guid id)
    {
        using var dbContext = _dbContext.CreateDbContext();
        return await dbContext.User.Where(x => x.UserId == id).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateUserAsync(Guid id, Users users)

    {
        using var dbContext = _dbContext.CreateDbContext();
        var userUpdate = await dbContext.User.FindAsync(users.UserId);

        if (userUpdate is null) return false;

        dbContext.Entry(userUpdate).CurrentValues.SetValues(users);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        using var dbContext = _dbContext.CreateDbContext();
        var userToDelete = await dbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

        if (userToDelete is null)
        {
            return false;
        }

        dbContext.User.Remove(userToDelete);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExistingUserAsync(string username, string email)

    {
        using var dbContext = _dbContext.CreateDbContext();
        return await dbContext.User.AnyAsync(x => x.Username == username || x.Email == email);
    }

}