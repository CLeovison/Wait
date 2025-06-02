using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Entities;


namespace Wait.UserServices.Services;


public interface IUserServices
{
    Task<bool> CreateUserAsync(UserDto userDto);
    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<List<Users>> PaginatedUserAsync(int page, int limit, string? searchTerm);
    Task<Users?> UpdateUserAsync(Guid id, UserDto userDto, CancellationToken ct);
    Task<bool> DeleteUserAsync(Guid id, CancellationToken ct);
}