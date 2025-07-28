
using Wait.Contracts.Response;

namespace Wait.Services.AuthService;

public interface IAuthService
{
    Task<LoginResponse> LoginUserAsync(string username, string password, CancellationToken ct);
    Task<LoginResponse> GetUserRefreshTokenByIdAsync(Guid id, CancellationToken ct);
    
}