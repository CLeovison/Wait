using Wait.Entities;
using Wait.Repositories;
using Wait.UserServices.Services;
using Wait.Mapping;
using Microsoft.AspNetCore.Identity;


namespace Wait.Services.UserServices;

public sealed class UserServices(IUserRepositories userRepositories) : IUserServices
{

    public async Task<bool> CreateUserAsync(Users users, IPasswordHasher<Users> passwordHasher)
    {
        var newUser = users.ToDto(passwordHasher);

        return await userRepositories.CreateUserAsync(newUser);
    }
}