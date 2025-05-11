using Wait.Contracts.Data;
using Wait.Contracts.Request.UserRequest;

namespace Wait.Mapping;

public static class RequestToDtoMapper
{
    public static UserDto ToRequest(this CreateUserRequest req)
    {
        return new UserDto
        {
            FirstName = req.FirstName,
            LastName = req.LastName,
            Username = req.Username,
            Password = req.Password,
            Email = req.Email
        };
    }
}