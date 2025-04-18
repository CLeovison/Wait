

using Wait.Entities;

namespace Wait.Services.UserServices;

public interface IUserServices
{
    Task CreateUserAsync(Users users);
    Task<List<Users>> GetAllUserAsync(CancellationToken ct);
    Task<bool> DeleteUserAsync(Guid id);

}