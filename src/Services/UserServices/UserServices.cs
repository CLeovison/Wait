
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

    public async Task<Users> CreateUserAsync(Users users, IPasswordHasher<Users> passwordHasher)
    {
        if (await _userRepositories.ExistingUserAsync(users.Username, users.Email))
        {
            throw new ArgumentException("The User Already exists");
        }

        var user = new Users
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Password = passwordHasher.HashPassword(users, users.Password),
            Email = users.Email
        };

        await _userRepositories.CreateUserAsync(user); // Save the hashed password

        return user;
    }

    public async Task<List<Users>> GetAllUserAsync(CancellationToken ct)
    {
        var getAllUser = await _userRepositories.GetAllUserAsync(ct);
        return getAllUser;
    }

    public async Task<Users?> UpdateUserAsync(Guid id, Users users)
    {
        var existingUser = await _userRepositories.GetUserIdAsync(id) ?? throw new ArgumentException("The user doesn't exist");

        existingUser.Username = users.Username;
        existingUser.Password = users.Password;
        existingUser.FirstName = users.FirstName;
        existingUser.LastName = users.LastName;
        existingUser.Email = users.Email;

        await _userRepositories.UpdateUserAsync(id, existingUser); // Assuming this method exists to save changes

        return existingUser;
    }
    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var userDelete = await _userRepositories.DeleteUserAsync(id);

        if (userDelete is null)
        {
            var message = $"This User {id} does not exists";
            throw new ArgumentNullException(message);
        }

        return userDelete is not null;
    }
}