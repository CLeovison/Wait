
using System.Security.Claims;
using Wait.Contracts.Response;

namespace Wait.Services.AuthService;

public interface IAuthService
{
    Task<AuthResponse> LoginUserAsync(string username, string password, CancellationToken ct);
    Task<AuthResponse> GetUserRefreshTokenAsync(AuthResponse response);
    Task<ClaimsPrincipal> GetClaimsPrincipalFromToken(string accessToken);
}