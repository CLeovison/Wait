namespace Wait.Services.AuthService;

public interface IAuthService
{
    Task<bool> LoginUserAsync(string username, string password, CancellationToken ct);
    Task LogoutUserAsync(Guid id, string refreshToken);
}