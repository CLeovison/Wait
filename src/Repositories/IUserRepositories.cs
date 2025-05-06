using Wait.Contracts.Data;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task<bool> CreateUserAsync(UserDto userDto);
    
}