using Wait.Entities;
using Wait.Repositories;
using Wait.UserServices.Services;
using Wait.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;


namespace Wait.Services.UserServices;

public sealed class UserServices(IUserRepositories userRepositories, IPasswordHasher<Users> passwordHasher) : IUserServices
{

    public async Task<bool> CreateUserAsync(UserDto userDto)
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
    public async Task<IEnumerable<Users>> PaginatedUserAsync(UserDto userDto)
    {
        int page = 1;
        int limits = 10;
        string? searchterm = "";


        if (limits > 0 && page > 0)
        {
            throw new ArgumentException("Invalid Limit and Page Provided");
        }
        var paginatedUser = userDto.ToEntities(passwordHasher);

        return await userRepositories.PaginatedUserAsync(paginatedUser, searchterm, limits, page);
    }
    public async Task<bool> UpdateUserAsync(Guid id, UserDto userDto, CancellationToken ct)
    {

        var toUpdateUser = userDto.ToEntities(passwordHasher);


        return await userRepositories.UpdateUserAsync(id, toUpdateUser, ct) is not null;

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