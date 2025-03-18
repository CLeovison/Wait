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

        if (user is not null)
        {
            throw new ArgumentException("The User Is Already Existed");
        }

        await dbContext.SaveChangesAsync(cancellationToken);

    }

}