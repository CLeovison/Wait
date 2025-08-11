using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Wait.Abstract;
using Wait.Contracts.Response;
using Wait.Domain.Entities;
using Wait.Infrastructure.Repositories;
using Wait.Infrastructure.Repositories.UserRepository;

namespace Wait.Services.AuthService;

public sealed class AuthService(IUserRepositories userRepositories,
IAuthRepository authRepository,
ITokenProvider tokenProvider,
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

        return new AuthResponse(accessToken, generateRefresh.Token);
    }

    public async Task<AuthResponse> GetUserRefreshTokenAsync(AuthResponse response)
    {
        var userTokenRotation = await authRepository.RefreshTokenRotationAsync(response.RefreshToken);

        if (userTokenRotation is null)
        {
            throw new ApplicationException("The RefreshToken is Expired");
        }

        if (userTokenRotation.ExpiresAt < DateTime.Now)
            throw new ApplicationException("The refresh token is expired.");

        var requestUser = userTokenRotation.User;

        if (requestUser is null)
        {
            throw new ApplicationException("Associated user not found for the refresh token.");
        }

        var principal = await tokenProvider.GetClaimsPrincipalFromToken(response.AccessToken);
        var username = principal.Identity?.Name;

        if (!string.Equals(username, requestUser.Username, StringComparison.OrdinalIgnoreCase))
        {
            throw new SecurityTokenArgumentException("Access token user does not match refresh token owner.");
        }

        string accessToken = tokenProvider.GenerateToken(requestUser);
        string refreshToken = tokenProvider.GenerateRefreshToken();

        userTokenRotation.Token = refreshToken;
        userTokenRotation.ExpiresAt = DateTime.UtcNow.AddDays(7);

        await authRepository.RefreshTokenUpdate(userTokenRotation);

        return new AuthResponse(accessToken, userTokenRotation.Token);
    }
}