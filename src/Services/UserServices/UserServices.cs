using Wait.Entities;
using Wait.Repositories;
using Wait.UserServices.Services;
using Wait.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Contracts.Request.UserRequest;


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

    public async Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
        var getUser = await userRepositories.GetUserByIdAsync(id, ct);

        if (getUser is null)
        {
            Results.NotFound();
        }

        return getUser;
    }

    public async Task<bool> UpdateUserAsync(UserDto userDto, IPasswordHasher<Users> passwordHasher, CancellationToken ct)
    {
        var updatedUser = userDto.ToEntities(passwordHasher);
        var result = await userRepositories.UpdateUserAsync(updatedUser.UserId, updatedUser, ct);
        return result != null;
    }
    public async Task<bool> DeleteUserAsync(Guid id, CancellationToken ct)
    {
        var existingUser = await userRepositories.GetUserByIdAsync(id, ct);

        if (existingUser is null)
        {
            throw new ArgumentException("User not found.");
        }

        return await userRepositories.DeleteUserAsync(existingUser);


    }
}