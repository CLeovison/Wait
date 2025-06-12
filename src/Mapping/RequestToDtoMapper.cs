using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Contracts.Request.UserRequest;
using Wait.Domain.Entities;

namespace Wait.Mapping;

public static class RequestToDtoMapper
{
    public static UserDto ToRequest(this CreateUserRequest req, IPasswordHasher<Users> passwordHasher)
    {
        return new UserDto
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Username = req.Username,
            Password = passwordHasher.HashPassword(new Users(), req.Password),
            Birthday = req.Birthday,
            Email = req.Email
        };
    }
    public static UserDto ToRequestUpdate(this UpdateUserRequest req, IPasswordHasher<Users> passwordHasher)
    {
        return new UserDto
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Username = req.Username,
            Password = passwordHasher.HashPassword(new Users(), req.Password),
            Birthday = req.Birthday,
            Email = req.Email
        };
    }
}