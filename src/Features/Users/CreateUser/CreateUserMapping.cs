namespace Wait.Features.Users.CreateUser;

public static class CreateUserMapping
{
    public static CreateUserRequest ToCreate(this Users users)
    {
        return new CreateUserRequest(
            users.Username,
            users.Password,
            users.FirstName,
            users.LastName,
            users.Email
        );
    }

    public static Users ToRequest(this CreateUserRequest request)
    {
        return new Users
        {
            Username = request.Username,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };



    }
}