
using Wait.Contracts.Response;

namespace Wait.Services.AuthService;

public interface IAuthService
{
    Task<AuthResponse> LoginUserAsync(string username, string password, CancellationToken ct);
    Task<AuthResponse> GetUserRefreshTokenAsync(string refreshToken, CancellationToken ct);
    
}