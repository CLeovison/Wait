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
    public async Task<LoginResponse> LoginUserAsync(string username, string password, CancellationToken ct)
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

        var generateRefresh = await authRepository.GenerateRefreshToken(refreshToken, ct);

        return new LoginResponse(accessToken, generateRefresh.Token);
    }
}