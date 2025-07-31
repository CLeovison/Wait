using Microsoft.AspNetCore.Identity;
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

    public async Task<AuthResponse> GetUserRefreshTokenAsync(string refreshToken, CancellationToken ct)
    {
        var userTokenRotation = await authRepository.RefreshTokenRotationAsync(refreshToken, ct);

        if (userTokenRotation is null || userTokenRotation.IsExpired)
        {
            throw new ApplicationException("The RefreshToken is Expired");
        }

        if (userTokenRotation.Token is null || userTokenRotation.Token.User is null)
        {
            throw new ApplicationException("Associated user not found for the refresh token.");
        }
        string accessToken = tokenProvider.GenerateToken(userTokenRotation.Token.User);

        userTokenRotation.Token.Token = tokenProvider.GenerateRefreshToken();
        userTokenRotation.Token.ExpiresAt = DateTime.UtcNow.AddDays(7);

        await authRepository.RefreshTokenUpdate(userTokenRotation.Token, ct);
        
        return new AuthResponse(accessToken, userTokenRotation.Token.Token);
    }


}