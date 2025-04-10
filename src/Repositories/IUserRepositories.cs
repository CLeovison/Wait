using Wait.Entities;

namespace Wait.Repositories;

public interface IUserRepositories
{
    Task CreateUserAsync(Users user, CancellationToken cancellationToken);
    Task<List<Users>> GetAllUserAsync(CancellationToken cancellationToken);
    Task GetSearchUserAsync(string searchTerm, CancellationToken cancellationToken);
    Task GetUserIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Users?> UpdateUserAsync(Users user, CancellationToken cancellationToken);
    Task<Users?> DeleteUserAsync(Guid id, CancellationToken cancellationToken);
}