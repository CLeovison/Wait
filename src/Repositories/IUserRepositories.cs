using Wait.Entities;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task<bool> CreateUserAsync(Users users);

}