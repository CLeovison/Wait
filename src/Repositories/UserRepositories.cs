using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Data;
using Wait.Database;
using Wait.Entities;

namespace Wait.Repositories;


public sealed class UserRepositories : IUserRepositories
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public UserRepositories(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<bool> CreateUserAsync(UserDto userDto)
    {
        var dbContext = _dbContextFactory.CreateDbContext();
        var createUser = await dbContext.Set<UserDto>().AddAsync(userDto);

        return createUser is not null;
    }

    
}