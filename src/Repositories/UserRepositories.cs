using Microsoft.EntityFrameworkCore;
using Wait.Database;
using Wait.Entities;

namespace Wait.Repositories;


public class UserRepositories : IUserRepositories
{
    private readonly AppDbContext _dbContext;

    public UserRepositories(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateUserAsync(Users user)
    {
        await _dbContext.Set<Users>().AddAsync(user);
        await _dbContext.SaveChangesAsync();

    }
    public async Task<List<Users>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.User.ToListAsync(cancellationToken);
    }

    public async Task<Users?> GetUsersByIdAsync(Guid id)
    {
        return await _dbContext.User.Where(x => x.UserId == id).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateUserAsync(Guid id, Users users)
    {
        var userUpdate = await _dbContext.User.FindAsync(users.UserId);

        if (userUpdate is null) return false;

        _dbContext.Entry(userUpdate).CurrentValues.SetValues(users);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var userToDelete = await _dbContext.User.FirstOrDefaultAsync(x => x.UserId == id);

        if (userToDelete is null)
        {
            return false;
        }

        _dbContext.User.Remove(userToDelete);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExistingUserAsync(string username, string email)
    {
        return await _dbContext.User.AnyAsync(x => x.Username == username || x.Email == email);
    }

}