using Wait.Contracts.Data;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Response;
using Wait.Contracts.Request.UserRequest;

namespace Wait.Services.UserServices;

public interface IUserServices
{
    Task<UserDto> CreateUserAsync(UserDto userDto, CancellationToken ct);

    Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken ct);

    Task<PaginatedResponse<UserDto>> GetPaginatedUsersAsync(PaginatedRequest req, FilterUserRequest filters, CancellationToken ct);
    Task<UserDto?> UpdateUserAsync(Guid id, UserDto users, CancellationToken ct);
    Task<bool> DeleteUserAsync(Guid id, CancellationToken ct);

}