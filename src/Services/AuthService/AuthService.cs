using Wait.Services.UserServices;

namespace Wait.Services.AuthService;


public sealed class AuthService(IUserServices userServices) : IAuthService
{
    public async Task<bool> LoginUserAsync(string username, string password)
    {
        
    }
}