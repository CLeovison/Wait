using Wait.Contracts.Data;
using Wait.Domain.UserDomain;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task<List<User>> GetAllUserAsync();
}