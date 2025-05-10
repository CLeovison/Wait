using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Entities;

namespace Wait.UserServices.Services;


public interface IUserServices
{
    Task<bool> CreateUserAsync(UserDto userDto);
}