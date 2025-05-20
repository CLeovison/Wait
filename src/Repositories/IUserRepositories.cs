using Wait.Contracts.Request.UserRequest;
using Wait.Entities;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task<bool> CreateUserAsync(Users users);
    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<Users?> UpdateUserAsync(Users users, CancellationToken ct);
    Task<bool> DeleteUserAsync(Users users);
}