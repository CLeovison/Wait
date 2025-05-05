
using Wait.Mapping;
using Microsoft.AspNetCore.Identity;

using Wait.Entities;
using Wait.Repositories;


namespace Wait.Services.UserServices;

public class UserServices : IUserServices
{
    private readonly IUserRepositories _userRepositories;


    public UserServices(IUserRepositories userRepositories)
    {
        _userRepositories = userRepositories;
    }

    public async Task<bool> CreateUserAsync(Users users, IPasswordHasher<Users> passwordHasher)
    {

        var existingUser = await _userRepositories.ExistingUserAsync(users.Username, users.Email);
        if (existingUser is true)
        {
            throw new ArgumentException("The User Already exists");
        }

        var userCreated = users.ToResponse();


        return await _userRepositories.CreateUserAsync(); // Save the hashed password


    }

    public async Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct)
    {
        var getAllUser = await _userRepositories.GetAllUsersAsync(ct);
        return getAllUser;
    }

    public async Task<Users?> UpdateUserAsync(Guid id, Users users)
    {
        var existingUser = await _userRepositories.GetUsersByIdAsync(id)
            ?? throw new ArgumentException("The user doesn't exist");


        existingUser.FirstName = users.FirstName;
        existingUser.LastName = users.LastName;
        existingUser.Username = users.Username;
        existingUser.Password = users.Password;
        existingUser.Email = users.Email;

        await _userRepositories.UpdateUserAsync(id, existingUser);

        return existingUser;
    }
    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var userDelete = await _userRepositories.DeleteUserAsync(id);

        if (!userDelete)
        {
            var message = $"This User {id} does not exists";
            throw new ArgumentNullException(message);
        }

        return userDelete is false;
    }
}