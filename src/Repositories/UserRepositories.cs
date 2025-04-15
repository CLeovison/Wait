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
    public async Task CreateUserAsync(Users user)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync();
        await dbContext.Set<Users>().AddAsync(user);

        await dbContext.SaveChangesAsync();

    }
    public async Task<List<Users>> GetAllUserAsync(CancellationToken cancellationToken)
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
    public async Task<Users?> GetUserIdAsync(Guid id)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync();
        return await dbContext.User.Where(x => x.UserId == id).FirstOrDefaultAsync();
    }

    public async Task<Users?> UpdateUserAsync(Users users, CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync(cancellationToken);
        var userUpdate = dbContext.User.Update(users);

        await dbContext.SaveChangesAsync(cancellationToken);
        return userUpdate.Entity;
    }
    public async Task<Users?> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
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