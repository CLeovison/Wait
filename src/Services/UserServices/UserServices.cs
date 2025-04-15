
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
    public async Task CreateUserAsync(Users user)
    {
        var existingUser = _userRepositories.GetUserIdAsync(user.UserId);

        if (existingUser is null)
        {
            var userMessage = $"A user with {user.UserId} already exists";
            throw new ArgumentException(userMessage);
        }

        await _userRepositories.CreateUserAsync(user);

    }
}