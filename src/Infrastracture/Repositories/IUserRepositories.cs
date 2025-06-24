using Wait.Domain.Entities;
using Wait.Contracts.Request.UserRequest;


namespace Wait.Infrastracture.Repositories;


public interface IUserRepositories
{
    Task<bool> CreateUserAsync(Users users);
    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<(List<Users>, int totalCount)> PaginatedUserAsync(FilterUserRequest filters, string? searchTerm,
         int skip,
         int take,
         string sortBy,
         bool desc,
         CancellationToken ct);
    Task<Users?> UpdateUserAsync(Users users, CancellationToken ct);
    Task<bool> DeleteUserAsync(Users users);
    Task<Users?> GetUserByUsernameAsync(string username);
}