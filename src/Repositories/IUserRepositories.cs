using Wait.Entities;

namespace Wait.Repositories;

public interface IUserRepositories
{
    Task CreateUserAsync(Users user);
    Task<List<Users>> GetAllUserAsync(CancellationToken cancellationToken);
    Task GetSearchUserAsync(string searchTerm, CancellationToken cancellationToken);
    Task<Users?> GetUserIdAsync(Guid id);
    Task<bool> UpdateUserAsync(Users user, CancellationToken cancellationToken);
    Task<Users?> DeleteUserAsync(Guid id);
    Task<bool> ExistingUserAsync(string username, string email);
}