using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Contracts.Request.UserRequest;
using Wait.Entities;

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

    public static Users ToUpdate(this UpdateUserRequest req, IPasswordHasher<Users> passwordHasher, Users users)
    {
        return new Users
        {
            UserId = req.UserId,
            FirstName = req.FirstName,
            LastName = req.LastName,
            Username = req.Username,
            Password = passwordHasher.HashPassword(users, req.Password),
            Email = req.Email,
            UpdatedAt = req.UpdatedAt
        };
    }
}