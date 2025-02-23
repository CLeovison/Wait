using Microsoft.EntityFrameworkCore.Internal;
using Wait.Contracts.Data;
using Wait.Database;

namespace Wait.Repositories;


public class UserRepositories : IUserRepositories
{

    private readonly AppDbContext _dbContext;


    public UserRepositories(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> CreateUser()
    {
        using var connection = await _dbContext.ConfigureConventions();
   
    }
}