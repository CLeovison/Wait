using Wait.Entities;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task<bool> CreateUserAsync(Users users);
    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Users>> PaginatedUserAsync(Users users, string? searchterm, int limit, int page);
    Task<Users?> UpdateUserAsync(Guid id, Users users, CancellationToken ct);
    Task<bool> DeleteUserAsync(Users users);
}