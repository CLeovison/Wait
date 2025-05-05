using Wait.Entities;

namespace Wait.Repositories;

public interface IUserRepositories
{
    Task<bool> CreateUserAsync(Users user);
    Task<IEnumerable<Users>> GetAllUsersAsync(CancellationToken cancellationToken);

    Task<Users?> GetUsersByIdAsync(Guid id);
    Task<bool> UpdateUserAsync(Guid id, Users user);
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> ExistingUserAsync(string username, string email);
}