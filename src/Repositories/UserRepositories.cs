using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Data;
using Wait.Database;


namespace Wait.Repositories;


public sealed class UserRepositories(IDbContextFactory<AppDbContext> dbContextFactory) : IUserRepositories
{
    public async Task<bool> CreateUserAsync(UserDto userDto)
    {
        var dbContext = dbContextFactory.CreateDbContext();
        var createUser = await dbContext.Set<UserDto>().AddAsync(userDto);

        return createUser is not null;
    }


}