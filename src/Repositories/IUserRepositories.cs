using Wait.Entities;

namespace Wait.Repositories;

public interface IUserRepositories
{
    Task CreateUserAsync(Users user);
    Task<List<Users>> GetAllUserAsync(CancellationToken cancellationToken);

    Task<Users?> GetUserIdAsync(Guid id);
    Task<bool> UpdateUserAsync(Guid id, Users user);
    Task<Users?> DeleteUserAsync(Guid id);
    Task<bool> ExistingUserAsync(string username, string email);
}