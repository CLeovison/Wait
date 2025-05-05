using Wait.Contracts.Request.UserRequest;
using Wait.Entities;

namespace Wait.Mapping;


public static class EntitiesToContractsMapper
{
    public static CreateUserRequest ToEntities(this Users users)
    {
        return new CreateUserRequest
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.LastName,
            Password = users.Password,
            Email = users.Email

        };
    }
}