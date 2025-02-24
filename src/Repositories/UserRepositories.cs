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

    public async Task<bool> CreateUser(UserDto user, CancellationToken cancellationToken)
    {
        using var dbContext = _dbContext.CreateDbContextAsync(cancellationToken);
    }

}