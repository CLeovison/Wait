using Wait.Entities;

namespace Wait.Repositories;

public interface IUserRepositories
{
    Task CreateUserAsync(Users user);
    Task<List<Users>> GetAllUsersAsync(CancellationToken cancellationToken);

    Task<Users?> GetUsersByIdAsync(Guid id);
    Task<bool> UpdateUserAsync(Guid id, Users user);
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> ExistingUserAsync(string username, string email);
}