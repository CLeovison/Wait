

using Microsoft.AspNetCore.Identity;
using Wait.Entities;

namespace Wait.Services.UserServices;

public interface IUserServices
{
    Task<bool> CreateUserAsync(Users users, IPasswordHasher<Users> passwordHasher);

    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users?> UpdateUserAsync(Guid id, Users users);
    Task<bool> DeleteUserAsync(Guid id);

}