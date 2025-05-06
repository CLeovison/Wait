using Wait.Contracts.Data;
using Wait.Entities;

namespace Wait.Mapping;


public static class EntitiesToDtoMapper
{
    public static Users ToEntities(this UserDto userDto)
    {
        return new Users
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Username = userDto.Username,
            Password = userDto.Password,
            Email = userDto.Email
        };
    }
}