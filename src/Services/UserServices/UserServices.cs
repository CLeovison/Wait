using Wait.Entities;
using Wait.Repositories;
using Wait.UserServices.Services;
using Wait.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Contracts.Response;
using Wait.Contracts.Request.Common;


namespace Wait.Services.UserServices;

public sealed class UserServices(IUserRepositories userRepositories, IPasswordHasher<Users> passwordHasher) : IUserServices
{

    public async Task<bool> CreateUserAsync(UserDto userDto)
    {
        var newUser = userDto.ToEntities(passwordHasher);

        if (!await userRepositories.CreateUserAsync(newUser))
            throw new ArgumentException("You didn't have any credentials");

        return true;
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
    public async Task<PaginatedResponse<Users>> PaginatedUserAsync(PaginatedRequest req, CancellationToken ct)
    {
        var getAllUser = await userRepositories.GetAllUserAsync(ct);

        //Validation for Filtering
        if (!string.IsNullOrWhiteSpace(req.SearchTerm))
        {
            getAllUser = getAllUser.Where(x => x.FirstName.Contains(req.SearchTerm!) || x.LastName.Contains(req.SearchTerm!));
        }

        //Validation for Sorting

        getAllUser = req.SortBy?.ToLower() switch
        {
            "firstname" => req.SortDirection ? getAllUser.OrderByDescending(x => x.FirstName) : getAllUser.OrderBy(x => x.FirstName),
            "lastname" => req.SortDirection ? getAllUser.OrderByDescending(x => x.LastName) : getAllUser.OrderBy(x => x.LastName),
            "email" => req.SortDirection ? getAllUser.OrderByDescending(x => x.Email) : getAllUser.OrderBy(x => x.Email),
            _ => getAllUser
        };

        //Validation for Pages
        if (req.Page < 0 || req.PageSize < 0)
        {
            throw new ArgumentException("Page number and page size must be greater than zero.");
        }
        return await userRepositories.PaginatedUserAsync(req.Page, req.PageSize);
    }

    public async Task<Users?> UpdateUserAsync(Guid id, Users users, CancellationToken ct)
    {
        var existingUser = await userRepositories.GetUserByIdAsync(id, ct);
        try
        {
            if (existingUser is null)
            {
                return null;
            }
            existingUser.FirstName = users.FirstName;
            existingUser.LastName = users.LastName;
            existingUser.Username = users.Username;
            existingUser.Password = passwordHasher.HashPassword(users, users.Password);
            existingUser.Email = users.Email;
            existingUser.ModifiedAt = users.ModifiedAt;

            return await userRepositories.UpdateUserAsync(existingUser, ct);
        }
        catch (Exception ex)
        {
            Results.NotFound(ex);
        }

        return existingUser;
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