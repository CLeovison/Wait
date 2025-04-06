using Wait.Entities;

namespace Wait.Repositories;

public interface IUserRepositories
{
    Task CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<List<User>> GetAllUserAsync(CancellationToken cancellationToken);
    Task GetSearchUserAsync(string searchTerm, CancellationToken cancellationToken);
    Task GetUserIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> UpdateUserAsync(User user, CancellationToken cancellationToken);
    Task<User?> DeleteUserAsync(Guid id, CancellationToken cancellationToken);
}