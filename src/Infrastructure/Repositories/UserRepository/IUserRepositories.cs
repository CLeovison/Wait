using Wait.Domain.Entities;
using Wait.Contracts.Request.UserRequest;


namespace Wait.Infrastructure.Repositories.UserRepository;


public interface IUserRepositories
{
    Task<Users> CreateUserAsync(Users users, CancellationToken ct);
    Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<(List<Users>, int totalCount)> GetPaginatedUserAsync(FilterUserRequest filters, string? searchTerm,
         int skip,
         int take,
         string sortBy,
         bool desc,
         CancellationToken ct);
    Task<Users?> UpdateUserAsync(Users users, CancellationToken ct);
    Task<bool> DeleteUserAsync(Users users, CancellationToken ct);
    Task<Users?> GetUserByUsernameAsync(string username, CancellationToken ct);
    Task<Users?> GetUserByEmailAsync(string email, CancellationToken ct);
}