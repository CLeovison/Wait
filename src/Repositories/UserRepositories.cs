using Microsoft.EntityFrameworkCore;
using Wait.Database;
using Wait.Entities;

namespace Wait.Repositories;


public class UserRepositories(IDbContextFactory<AppDbContext> _dbContext) : IUserRepositories
{

    public async Task CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync(cancellationToken);
        await dbContext.Set<User>().AddAsync(user, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

    }
    public async Task<List<User>> GetAllUserAsync(CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync(cancellationToken);
        return await dbContext.User.ToListAsync();
    }

    public async Task GetSearchUserAsync(string searchTerm, CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync(cancellationToken);

        var searchUser = await dbContext.User
        .Where(x => x.Username.Contains(searchTerm)
        || x.FirstName.Contains(searchTerm)
        || x.LastName.Contains(searchTerm))
        .Select(n => new
        {
            n.Username,
            n.FirstName,
            n.LastName,

        }).ToListAsync();
    }
    public async Task GetUserIdAsync(Guid id, CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync(cancellationToken);
        await dbContext.User.FindAsync(id);
    }

    public async Task<User?> UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync(cancellationToken);
        var userUpdate = dbContext.User.Update(user);

        await dbContext.SaveChangesAsync(cancellationToken);
        return userUpdate.Entity;
    }
    public async Task<User?> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync(cancellationToken);
        var userToDelete = await dbContext.User.FirstOrDefaultAsync(x => x.UserId == id, cancellationToken);

        if (userToDelete != null)
        {
            dbContext.User.Remove(userToDelete);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        return userToDelete;
    }

}