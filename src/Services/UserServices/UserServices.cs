
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

    public async Task CreateUserAsync(Users users)
    {

        if (await _userRepositories.ExistingUserAsync(users.Username, users.Email))
        {
            throw new ArgumentException("The User Already exist");
        }

        await _userRepositories.CreateUserAsync(users);

    }

    public async Task<List<Users>> GetAllUserAsync(CancellationToken ct)
    {
        var getAllUser = await _userRepositories.GetAllUserAsync(ct);
        return getAllUser;
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