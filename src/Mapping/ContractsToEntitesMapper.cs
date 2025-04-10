using Wait.Contracts.Request.UserRequest;
using Wait.Entities;

namespace Wait.Mapping;


public static class ContractsToEntitesMapper
{

    public static Users ToCreateUser(this CreateUserRequest request)
    {

        return new Users
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Password = request.Password,
            Email = request.Email,

        };
    }

    public static Users ToUpdateUser(this UpdateUserRequest request)
    {
        return new Users
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Password = request.Password,
            Email = request.Email,
        };
    }

}