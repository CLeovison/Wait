using Wait.Contracts.Data;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task CreateUser(UserDto user, CancellationToken cancellationToken);
}