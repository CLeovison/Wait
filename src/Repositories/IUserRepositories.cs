using Wait.Entities;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task<bool> CreateUserAsync(Users users);
    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users?> GetUserByIdAsync(Guid id);
    Task<bool> DeleteUserAsync(Users users);
}