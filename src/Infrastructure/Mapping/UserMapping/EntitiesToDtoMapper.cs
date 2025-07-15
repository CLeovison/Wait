using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Mapping;


public static class EntitiesToDtoMapper
{
    public static Users ToEntities(this UserDto userDto, IPasswordHasher<Users> passwordHasher)
    {
        return new Users
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Username = userDto.Username,
            Password = passwordHasher.HashPassword(new Users(), userDto.Password),
            Birthday = DateOnly.Parse(userDto.Birthday.ToString()),
            Email = userDto.Email,
            ModifiedAt = userDto.ModifiedAt
        };
    }

}