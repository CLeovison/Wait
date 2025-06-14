using Wait.Domain.Entities;
using Wait.Contracts.Response;


namespace Wait.Infrastracture.Repositories;


public interface IUserRepositories
{
    Task<bool> CreateUserAsync(Users users);
    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Users>> SearchUserAsync(string? searchTerm, CancellationToken ct);
    Task<PaginatedResponse<Users>> PaginatedUserAsync(int page, int pageSize);
    Task<Users?> UpdateUserAsync(Users users, CancellationToken ct);
    Task<bool> DeleteUserAsync(Users users);
}