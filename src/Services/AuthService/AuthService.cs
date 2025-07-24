using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Contracts.Data;
using Wait.Infrastructure.Repositories.UserRepository;


namespace Wait.Services.AuthService;


public sealed class AuthService(IUserRepositories userRepositories, ITokenProvider tokenProvider, IPasswordHasher<UserDto> passwordHasher) : IAuthService
{
    public async Task<bool> LoginUserAsync(string username, string password, CancellationToken ct)
    {
        var userRequest = await userRepositories.GetUserByUsernameAsync(username, ct);

        if (userRequest is null)
        {
            throw new UnauthorizedAccessException("Invalid Credntials");
        }

        var accessToken = tokenProvider.GenerateToken(userRequest);
        var refreshToken = tokenProvider.GenerateRefreshToken();


        return accessToken is not null;
    }
}