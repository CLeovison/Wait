using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Wait.Contracts.Data;
using Wait.Database;

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
    public async Task CreateUser(UserDto user, CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContext.CreateDbContextAsync(cancellationToken);
        await dbContext.Set<UserDto>().AddAsync(user, cancellationToken);

        if (user is not null)
        {
            throw new ArgumentException("The User Is Already Existed");
        }

        await dbContext.SaveChangesAsync(cancellationToken);

    }

}