using Wait.Contracts.Data;


namespace Wait.UserServices.Services;


public interface IUserServices
{
    Task<bool> CreateUserAsync(UserDto userDto);
}