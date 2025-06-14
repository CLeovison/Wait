using Wait.Contracts.Response.UserResponse;
using Wait.Domain.Entities;

namespace Wait.Infrastracture.Mapping;


public static class EntitiesToContractsMapper
{
    public static UserResponse ToUserResponse(this Users users)
    {
        return new UserResponse
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Email = users.Email
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