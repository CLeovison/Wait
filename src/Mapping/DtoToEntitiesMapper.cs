using Wait.Contracts.Data;
using Wait.Entities;

namespace Wait.Mapping;


public static class DtoToEntitiesMapper
{
    public static UserDto ToDto(this Users users)
    {
        return new UserDto
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Password = users.Password,
            Email = users.Email
        };
    }
}