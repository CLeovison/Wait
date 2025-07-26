using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Contracts.Data;
using Wait.Contracts.Response;
using Wait.Database;
using Wait.Domain.Entities;
using Wait.Infrastructure.Repositories.UserRepository;

namespace Wait.Services.AuthService;


public sealed class AuthService(IUserRepositories userRepositories,
ITokenProvider tokenProvider,
IPasswordHasher<Users> passwordHasher,
AppDbContext dbContext) : IAuthService
{
    public async Task<LoginResponse> LoginUserAsync(string username, string password, CancellationToken ct)
    {
        var userRequest = await userRepositories.GetUserByUsernameAsync(username, ct);

        if (userRequest is null)
        {
            throw new ApplicationException("The User Was Not Found");
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
        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync();


        return new LoginResponse(

            accessToken,
            refreshToken.Token
        );
    }
}