using Wait.Domain.Entities;
using Wait.Infrastracture.Repositories;
using Wait.Infrastracture.Mapping;
using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Contracts.Response;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Request.UserRequest;
using Wait.Helper;
using Microsoft.AspNetCore.Http.HttpResults;


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

    public async Task<PaginatedResponse<Users>> PaginatedUsersAsync(PaginatedRequest req, FilterUserRequest filters, CancellationToken ct)
    {
        var pagination = PaginationProcessor.Create(req);

        var (users, totalCount) = await userRepositories.PaginatedUserAsync(filters,
            req.SearchTerm, pagination.Skip, pagination.Take,
            pagination.SortBy, pagination.Descending, ct);

        return new PaginatedResponse<Users>(users, pagination.Page, pagination.PageSize, totalCount);
    }


    public async Task<Users?> UpdateUserAsync(Guid id, UserDto users, CancellationToken ct)
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
            existingUser.Password = passwordHasher.HashPassword(existingUser, users.Password);
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

    public async Task<Users?> LoginUserAsync(string username, string password)
    {
        var existingUser = await userRepositories.GetUserByUsernameAsync(username);

        if (existingUser == null || existingUser.Username != username)
        {
            return null;
        }
        var verifiedPassword = passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, password);

    }
}