using Wait.Contracts.Response;

namespace Wait.Services.AuthService;

public interface IAuthService
{
    Task<AuthResponse> LoginUserAsync(string username, string password, CancellationToken ct);
    Task<AuthResponse?> RefreshTokenAsync(string expiredAccessToken, string refreshToken);

    Task<bool> RevokeRefreshTokenAsync(Guid id, CancellationToken ct);
    
    
    // Task<bool> SendEmailVerificationAsync();
    // Task<bool> VerificationEmailAsync();
    // Task<bool> ForgotPasswordAsync();
    // Task<bool> ResetPasswordAsync();

}