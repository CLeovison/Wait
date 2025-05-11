using Wait.Entities;
using Wait.Repositories;
using Wait.UserServices.Services;
using Wait.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;


namespace Wait.Services.UserServices;

public sealed class UserServices(IUserRepositories userRepositories) : IUserServices
{

    public async Task<bool> CreateUserAsync(UserDto userDto, IPasswordHasher<Users> passwordHasher)
    {
        var newUser = userDto.ToEntities(passwordHasher);

        return await userRepositories.CreateUserAsync(newUser);
    }

    public async Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct)
    {
        return await userRepositories.GetAllUserAsync(ct);
    }
}