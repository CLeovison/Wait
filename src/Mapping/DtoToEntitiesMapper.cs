using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Entities;

namespace Wait.Mapping;


public static class DtoToEntitiesMapper
{
    public static UserDto ToDto(this Users users, IPasswordHasher<Users> passwordHasher)
    {
        return new UserDto
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Password = passwordHasher.HashPassword(new Users(), users.Password),
            Email = users.Email
        };
    }
}