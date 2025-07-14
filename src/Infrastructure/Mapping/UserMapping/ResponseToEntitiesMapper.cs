using Wait.Contracts.Data;
using Wait.Contracts.Response.UserResponse;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Mapping;


public static class ResponseToEntitiesMapper
{
    public static UserResponse ToUserResponse(this UserDto users)
    {
        return new UserResponse
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Email = users.Email,
            Birthday = users.Birthday
        };
    }

    public static GetAllUserResponse ToGetAllUserResponse(this IEnumerable<Users> users)
    {
        return new GetAllUserResponse
        {
            Users = users.Select(x => new UserResponse
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.Username,
                Birthday = x.Birthday,
                Email = x.Email
            })
        };
    }
}