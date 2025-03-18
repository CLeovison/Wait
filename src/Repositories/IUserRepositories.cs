using Wait.Entities;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task CreateUserAsync(User user, CancellationToken cancellationToken);
}