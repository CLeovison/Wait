using Microsoft.AspNetCore.Identity;
using Wait.Domain.Entities;

using Wait.Contracts.Data;
using Wait.Contracts.Response;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Request.UserRequest;

using Wait.Helper;

using Wait.Infrastructure.Mapping;
using Wait.Infrastructure.Repositories.UserRepository;
using Wait.Infrastructure.Authentication;


namespace Wait.Services.UserServices;

public sealed class UserServices(IUserRepositories userRepositories, IPasswordHasher<Users> passwordHasher, TokenProvider tokenProvider) : IUserServices
{

    public async Task<UserDto> CreateUserAsync(UserDto userDto, CancellationToken ct)
    {
        var newUser = userDto.ToEntities(passwordHasher);

        var existingUser = await userRepositories.GetUserByUsernameAsync(userDto.Username, ct);

        if (existingUser is not null)
        {
            throw new ArgumentException("The user is already existing");
        }

        var result = await userRepositories.CreateUserAsync(newUser, ct);
        UserDto newUsers = result.ToDto(passwordHasher);

        return newUsers;
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

        if (existingUser is null)
        {
            return null;
        }

        try
        {
            existingUser.FirstName = users.FirstName;
            existingUser.LastName = users.LastName;
            existingUser.Username = users.Username;
            if (!string.IsNullOrWhiteSpace(users.Password))
            {
                existingUser.Password = passwordHasher.HashPassword(existingUser, users.Password);
            }
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

        return await userRepositories.DeleteUserAsync(existingUser, ct);
    }

    public async Task<string?> LoginUserAsync(string username, string password, CancellationToken ct)
    {
        var existingUser = await userRepositories.GetUserByUsernameAsync(username, ct);

        if (existingUser == null)
        {
            return null;
        }

        bool verifiedPassword = passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, password) == PasswordVerificationResult.Success;

        if (verifiedPassword)
        {
            throw new ArgumentException("The password that you've provide is incorrect");
        }

        var token = tokenProvider.Create(existingUser);

        return token;
    }


}

