using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Request.UserRequest;
using Wait.Domain.Entities;

namespace Wait.Mapping;

public static class ContractsToEntitiesMapper
{

    public static Users ToCreate(this CreateUserRequest req)
    {
        var user = new Users
        {
            UserId = Guid.NewGuid(),
            FirstName = req.FirstName,
            LastName = req.LastName,
            Username = req.Username,
            Password = req.Password,
            Email = req.Email,
            CreatedAt = req.CreatedAt
        };

        return user;
    }

    public static Users ToUpdate(this UpdateUserRequest req, IPasswordHasher<Users> passwordHasher)
    {
        return new Users
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Username = req.Username,
            Password = passwordHasher.HashPassword(new Users(), req.Password),
            Email = req.Email,

        };
    }
}