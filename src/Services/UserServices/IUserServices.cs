

using Wait.Entities;

namespace Wait.Services.UserServices;

public interface IUserServices
{
    Task CreateUserAsync(Users user);

}