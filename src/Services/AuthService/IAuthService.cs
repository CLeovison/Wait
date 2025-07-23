namespace Wait.Services.AuthService;

public interface IAuthService
{
    Task<bool> Login(string username, string password, CancellationToken ct);
    Task Logout(Guid id, string refreshToken);
}