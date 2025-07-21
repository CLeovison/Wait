using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories.AuthRepository;


public interface IAuthRepository
{
    Task<Users> LoginUserAsync(string username, string password);
    Task SendPasswordResetEmail(string email);
    Task SendPasswordVerificationToken();
    Task<Users> ResetPasswordAsync();
}