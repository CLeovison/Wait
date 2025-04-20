

using Microsoft.AspNetCore.Identity;
using Wait.Entities;

namespace Wait.Services.UserServices;

public interface IUserServices
{
    Task<Users> CreateUserAsync(Users users, IPasswordHasher<Users> passwordHasher);
    Task<List<Users>> GetAllUserAsync(CancellationToken ct);
    Task<bool> DeleteUserAsync(Guid id);

}