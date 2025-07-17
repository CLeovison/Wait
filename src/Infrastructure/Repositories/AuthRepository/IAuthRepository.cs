using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories.AuthRepository;


public interface IAuthRepository
{
    Task<Users> LoginUserAsync();
    Task<Users> ResetPassword();
}