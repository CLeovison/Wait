namespace Wait.Services.AuthService;

public interface IAuthService
{
    Task GenerateRefreshToken();
    Task GenerateToken();
    Task<bool> InvokeRefreshToken();
}