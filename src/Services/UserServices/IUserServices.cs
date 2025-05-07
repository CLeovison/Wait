using Microsoft.AspNetCore.Identity;
using Wait.Entities;

namespace Wait.UserServices.Services;


public interface IUserServices
{
    Task<bool> CreateUserAsync(Users users, IPasswordHasher<Users> passwordHasher);
}