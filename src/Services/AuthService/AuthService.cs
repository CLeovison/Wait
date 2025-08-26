using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Wait.Abstract;
using Wait.Contracts.Response;
using Wait.Domain.Entities;
using Wait.Extensions;
using Wait.Infrastructure.Repositories;
using Wait.Infrastructure.Repositories.UserRepository;

namespace Wait.Services.AuthService;

public sealed class AuthService(IUserRepositories userRepositories,
IAuthRepository authRepository,
ITokenProvider tokenProvider,
IHttpContextAccessor httpContext,
IPasswordHasher<Users> passwordHasher) : IAuthService
{
    public async Task<AuthResponse> LoginUserAsync(string username, string password, CancellationToken ct)
    {
        var userRequest = await userRepositories.GetUserByUsernameAsync(username, ct);

        if (userRequest is null)
        {
            throw new ApplicationException("The user does not exist!");
        }

        var verificationResult = passwordHasher.VerifyHashedPassword(userRequest, userRequest.Password, password);
        bool verifiedPassword = verificationResult == PasswordVerificationResult.Success;

        if (!verifiedPassword)
        {
            throw new ApplicationException("Invalid password.");
        }

        var accessToken = tokenProvider.GenerateToken(userRequest);
        var refreshToken = new RefreshToken
        {
            TokenId = Guid.NewGuid(),
            Token = tokenProvider.GenerateRefreshToken(),
            UserId = userRequest.UserId,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow
        };

        var generateRefresh = await authRepository.SaveRefreshTokenAsync(refreshToken, ct);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = generateRefresh.Token
        };
    }

    public async Task<AuthResponse?> RefreshTokenAsync(string expiredAccessToken, string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(expiredAccessToken))
            throw new ArgumentException("Access token is missing.", nameof(expiredAccessToken));

        if (string.IsNullOrWhiteSpace(refreshToken))
            throw new ArgumentException("Refresh token is missing.", nameof(refreshToken));

        var userTokenRotation = await authRepository.RefreshTokenRotationAsync(refreshToken);

        if (userTokenRotation is null)
            throw new ApplicationException("Unable to retrieve user for refresh token");

        if (userTokenRotation.ExpiresAt < DateTime.UtcNow)
            throw new ApplicationException("The refresh token is expired.");

        var requestUser = userTokenRotation.User ?? throw new ApplicationException("Associated user not found for the refresh token.");

        var principal = tokenProvider.GetClaimsPrincipalFromToken(expiredAccessToken);
        var username = principal.GetUsername();

        if (!string.Equals(username, requestUser.Username, StringComparison.OrdinalIgnoreCase))
            throw new SecurityTokenArgumentException("Access token user does not match refresh token owner.");

        string newAccessToken = tokenProvider.GenerateToken(requestUser);
        string newRefreshToken = tokenProvider.GenerateRefreshToken();

        userTokenRotation.Token = newRefreshToken;
        userTokenRotation.ExpiresAt = DateTime.UtcNow.AddDays(7);

        await authRepository.RefreshTokenUpdate(userTokenRotation);

        httpContext.WriteTokenAsHttpOnlyCookie("accessToken", newAccessToken, DateTime.Now.AddMinutes(15));
        httpContext.WriteTokenAsHttpOnlyCookie("refreshToken", newRefreshToken, DateTime.Now.AddDays(7));
        return new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = userTokenRotation.Token
        };
    }
    public async Task<bool> RevokeRefreshTokenAsync(Guid id, CancellationToken ct)
    {
        var currentUser = UserCredentials();

        if (currentUser is null)
        {
            throw new SecurityTokenArgumentException("The User doesn't exist");
        }

        if (currentUser != id)
        {
            throw new SecurityTokenValidationException("The User Doesn't Match");
        }

        var deleteUser = await authRepository.RevokeRefreshTokenAsync(id, ct);

        if (!deleteUser)
        {
            throw new SecurityTokenException("No active refresh tokens found for this user.");
        }

        return true;
    }

    private Guid? UserCredentials()
    {
        return Guid.TryParse(httpContext?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid parsed) ? parsed : null;
    }

}