using Microsoft.AspNetCore.Identity;
using Wait.Contracts.Data;
using Wait.Entities;

namespace Wait.Mapping;


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
            Birthday = userDto.Birthday,
            Email = userDto.Email,
            ModifiedAt = userDto.ModifiedAt
        };
    }


}