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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task CreateUser(User user, CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync(cancellationToken);
        await dbContext.Set<User>().AddAsync(user, cancellationToken);

        if (user is not null)
        {
            throw new ArgumentException("The User Is Already Existed");
        }

        await dbContext.SaveChangesAsync(cancellationToken);

    }

}