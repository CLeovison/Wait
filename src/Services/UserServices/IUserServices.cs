using Wait.Contracts.Data;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Response;
using Wait.Domain.Entities;
using Wait.Contracts.Request.UserRequest;

namespace Wait.Services.UserServices;

public interface IUserServices
{
    Task<UserDto> CreateUserAsync(UserDto userDto, CancellationToken ct);

    Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct);

    Task<PaginatedResponse<Users>> GetPaginatedUsersAsync(PaginatedRequest req, FilterUserRequest filters, CancellationToken ct);
    Task<Users?> UpdateUserAsync(Guid id, UserDto users, CancellationToken ct);
    Task<bool> DeleteUserAsync(Guid id, CancellationToken ct);

}