using Wait.Contracts.Data;

namespace Wait.Repositories;


public interface IUserRepositories
{
    Task<bool> CreateUser(UserDto user, CancellationToken cancellationToken);
}