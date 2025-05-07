using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Request.UserRequest;
using Wait.Entities;

namespace Wait.Mapping;

public static class ContractsToEntitiesMapper
{

    public static CreateUserRequest ToCreate(this Users users, IPasswordHasher<Users> passwordHasher)
    {
        return new CreateUserRequest
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Password = passwordHasher.HashPassword(users, users.Password),
            Email = users.Email
        };
    }

    public static UpdateUserRequest ToUpdate(this Users users, IPasswordHasher<Users> passwordHasher)
    {
        return new UpdateUserRequest
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Password = passwordHasher.HashPassword(users, users.Password),
            Email = users.Email
        };
    }
}