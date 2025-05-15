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
        var createdUser = await userRepositories.CreateUserAsync(newUser);

        if (!createdUser)
        {
            throw new ArgumentException("You didn't have any credentials ");
        }
        else
        {
            return createdUser;
        }

    }

    public async Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct)
    {
        var result = await userRepositories.GetAllUserAsync(ct);
        return result;
    }

    public async Task<Users?> DeleteUserAsync(Guid id)
    {
        var deleteUser = await userRepositories.DeleteUserAsync(id);


        if (deleteUser is null)
        {
            return null;
        }

        return deleteUser;
    }
}