using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Entities;


namespace Wait.UserServices.Services;


public interface IUserServices
{
    Task<bool> CreateUserAsync(UserDto userDto, IPasswordHasher<Users> passwordHasher);
    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Users>> PaginatedUserAsync(UserDto userDto, DateTime? timestamp, IPasswordHasher<Users> passwordHasher, int page, int limit);
    Task<bool> UpdateUserAsync(Guid id, UserDto userDto, IPasswordHasher<Users> passwordHasher, CancellationToken ct);
    Task<bool> DeleteUserAsync(Guid id, CancellationToken ct);
}