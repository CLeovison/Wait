using Wait.Entities;
using Wait.Repositories;
using Wait.UserServices.Services;
using Wait.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;


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
    public async Task<List<Users>> PaginatedUserAsync(int page, int limit, string? searchTerm)
    {

        var userPaginated = await userRepositories.PaginatedUserAsync(limit, page);

        if (limit < 0 || page < 0)
        {
            throw new ArgumentException("Invalid Limit and Page Provided");
        }

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            userPaginated = userPaginated.Where(x => x.FirstName.Contains(searchTerm) || x.LastName.Contains(searchTerm)).ToList();
        }

        return userPaginated;
    }
    public async Task<Users?> UpdateUserAsync(Guid id, Users users, CancellationToken ct)
    {


        var existingUser = await userRepositories.GetUserByIdAsync(id, ct);

        if (existingUser is null)
        {
            return null;
        }

        existingUser.FirstName = users.FirstName;
        existingUser.LastName = users.LastName;
        existingUser.Username = users.Username;
        existingUser.Password = passwordHasher.HashPassword(users, users.Password);
        existingUser.Email = users.Email;

        return await userRepositories.UpdateUserAsync(existingUser, ct);

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