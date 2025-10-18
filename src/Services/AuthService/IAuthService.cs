using Wait.Contracts.Response;

namespace Wait.Services.AuthService;

public interface IAuthService
{
    // Task<RegisterUserDto> RegisterUserAsync();
    Task LoginUserAsync(string username, string password, CancellationToken ct);
    Task RefreshTokenAsync(string expiredAccessToken, string refreshToken);

    Task<bool> RevokeRefreshTokenAsync(Guid id, CancellationToken ct);


    // Task<bool> SendEmailVerificationAsync();
    //
    // Task<bool> VerificationEmailAsync();
    // Task<bool> ForgotPasswordAsync();
    // Task<bool> ResetPasswordAsync();

}
