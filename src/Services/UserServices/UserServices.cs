using Wait.Domain.Entities;
using Wait.Infrastracture.Repositories;
using Wait.Infrastracture.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Contracts.Response;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Request.UserRequest;


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

    public async Task<IEnumerable<Users>> SearchUserAsync(string? searchTerm, CancellationToken ct)
    {
        var lowerCaseTerm = searchTerm?.Trim().ToLower();

        if (string.IsNullOrWhiteSpace(lowerCaseTerm))
        {
            Enumerable.Empty<Users>();
        }

        return await userRepositories.SearchUserAsync(lowerCaseTerm, ct);
    }
    public async Task<PaginatedResponse<Users>> PaginatedUserAsync(PaginatedRequest req, FilterUserRequest filter, CancellationToken ct)
    {
        if (req.Page < 1)
        {
            req.Page = 1;
        }

        if (req.PageSize < 1 || req.PageSize > 100)
        {
            req.PageSize = 10;
        }
        
        var result = await userRepositories.PaginatedUserAsync(req, filter, ct);
        return result;
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