
using Wait.Contracts.Response.UserResponse;
using Wait.Entities;



namespace Wait.Mapping;


public static class EntitiesToContractsMapper
{
    public static UserResponse ToResponse(this Users users)
    {
        return new UserResponse
        {
            FirstName = users.FirstName,
            LastName = users.LastName,
            Username = users.Username,
            Email = users.Email
        };
    }
}