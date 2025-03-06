using Wait.Contracts.Data;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task<List<>> GetAllUserAsync();
}