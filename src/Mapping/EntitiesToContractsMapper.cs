using Wait.Contracts.Response.UserResponse;
using Wait.Entities;

namespace Wait.Mapping;


public static class EntitiesToContractsMapper
{
    public static UserResponse ToUserResponse(this Users users)
    {
        return new UserResponse
        {
            UserId = users.UserId,
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Password = users.Password,
            Email = users.Email
        };
    }

    public static GetAllUserResponse ToGetAllUserResponse(this IEnumerable<Users> users)
    {
        return new GetAllUserResponse
        {
            Users = users.Select()
        };
    }
}