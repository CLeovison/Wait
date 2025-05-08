using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Request.UserRequest;
using Wait.Entities;

namespace Wait.Mapping;

public static class ContractsToEntitiesMapper
{

    public static Users ToCreate(this CreateUserRequest req, IPasswordHasher<Users> passwordHasher, Users users)
    {
        return new Users
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Username = req.Username,
            Password = passwordHasher.HashPassword(users ,req.Password),
            Email = req.Email
        };
    }

    public static Users ToUpdate(this UpdateUserRequest req, IPasswordHasher<Users> passwordHasher, Users users)
    {
        return new Users
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Username = req.Username,
            Password = passwordHasher.HashPassword(users, req.Password),
            Email = req.Email
        };
    }
}