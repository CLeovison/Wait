
using Wait.Contracts.Data;
using Wait.Contracts.Request.Shared;
using Wait.Contracts.Response;
using Wait.Entities;



namespace Wait.UserServices.Services;


public interface IUserServices
{
    Task<bool> CreateUserAsync(UserDto userDto);
    Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct);
    Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct);
    Task<PaginatedResponse<Users>> PaginatedUserAsync(PaginatedRequest req, CancellationToken ct);
    Task<Users?> UpdateUserAsync(Guid id, Users users, CancellationToken ct);
    Task<bool> DeleteUserAsync(Guid id, CancellationToken ct);
}