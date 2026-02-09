using Microsoft.AspNetCore.Identity;
using Wait.Entities;

namespace Wait.Features.Users.CreateUser;

public static class CreateUserMapping
{

    public static User ToEntity(this CreateUserRequest request, IPasswordHasher<User> passwordHasher)
    {
        var user = new User
        {
            Username = request.Username,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        user.Password = passwordHasher.HashPassword(user, request.Password);

        return user;


    }
}