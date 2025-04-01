using Wait.Contracts.Request.UserRequest;
using Wait.Entities;

namespace Wait.Mapping;


public static class ContractsToEntitesMapper
{

    public static User ToCreateUser(this CreateUserRequest request)
    {

        return new User
        {
            UserId = request.UserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Password = request.Password,
            Email = request.Email,

        };
    }

    public static User ToUpdateUser(this UpdateUserRequest request)
    {
        return new User
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