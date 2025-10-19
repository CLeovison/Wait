using Wait.Contracts.Data;
using Wait.Contracts.Response.UserResponse;
using Wait.Domain.Entities;
using Wait.Contracts.Response;
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

    public static UserResponse ToPaginatedUserResponse(this PaginatedResponse<UserDto> paginated)
    {
        return new UserResponse
        {
            FirstName = paginated.Data.FirstOrDefault()?.FirstName,
            LastName = paginated.Data.FirstOrDefault()?.LastName,
            Username = paginated.Data.FirstOrDefault()?.Username,
            Email = paginated.Data.FirstOrDefault()?.Email,

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
