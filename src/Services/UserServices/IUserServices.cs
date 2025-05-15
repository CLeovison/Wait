using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Entities;


namespace Wait.UserServices.Services;


public interface IUserServices
{
    Task<bool> CreateUserAsync(UserDto userDto, IPasswordHasher<Users> passwordHasher);
    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users> GetUserByIdAsync(Guid id);
    Task<bool> DeleteUserAsync(Users users);
}