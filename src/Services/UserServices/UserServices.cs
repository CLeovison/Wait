using Microsoft.AspNetCore.Identity;
using Wait.Domain.Entities;
using Wait.Contracts.Data;
using Wait.Contracts.Response;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Request.UserRequest;
using Wait.Helper;
using Wait.Infrastructure.Mapping;
using Wait.Infrastructure.Repositories.UserRepository;

namespace Wait.Services.UserServices;

public sealed class UserServices(IUserRepositories userRepositories, IPasswordHasher<Users> passwordHasher) : IUserServices
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
        return result.ToUserDto(passwordHasher);
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
        var user = await userRepositories.GetUserByIdAsync(id, ct);
        try
        {
            if (user is null)
            {
                throw new ArgumentException($"User with Id {id} was not found");
            }

            return user.ToUserDto(passwordHasher);
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException("The User Does not Exist",ex);
        }
    }

    public async Task<PaginatedResponse<UserDto>> GetPaginatedUsersAsync(PaginatedRequest req, FilterUserRequest filters, CancellationToken ct)
    {
        var defaultSort = SortDefaults.GetDefaultSortField("FirstName");
        var pagination = PaginationProcessor.Create(req, defaultSort);

        var (users, totalCount) = await userRepositories.GetPaginatedUserAsync(filters,
            req.SearchTerm, pagination.Skip, pagination.Take,
            pagination.EffectiveSortBy, pagination.Descending, ct);

        var userMap = users.Select(x => x.ToUserDto(passwordHasher)).ToList();

        return new PaginatedResponse<UserDto>(userMap, pagination.Page, pagination.PageSize, totalCount);

    }

    public async Task<UserDto?> UpdateUserAsync(Guid id, UserDto users, CancellationToken ct)
    {
        var existingUser = await userRepositories.GetUserByIdAsync(id, ct);

        if (existingUser is null)
        {
            return null;
        }

        try
        {
            existingUser.ToUpdateDto(users, passwordHasher);
            var updatedUser = await userRepositories.UpdateUserAsync(existingUser, ct);
            return updatedUser?.ToUserDto(passwordHasher);
        }
        catch (Exception ex)
        {
            Results.NotFound(ex);
        }

        return existingUser?.ToUserDto(passwordHasher);

    }
    
    public async Task<bool> DeleteUserAsync(Guid id, CancellationToken ct)
    {
        var existingUser = await userRepositories.GetUserByIdAsync(id, ct);

        try
        {
            if (existingUser is null)
            {
                throw new ArgumentException("User not found.");
            }

            return await userRepositories.DeleteUserAsync(existingUser, ct);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("ex", ex);
        }

    }

}

