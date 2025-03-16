using Wait.Entities;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken);
    Task<User> GetSearchUserAsync();
    Task<List<User>> GetAllUserAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task DeleteUserAsync(Guid id);

}