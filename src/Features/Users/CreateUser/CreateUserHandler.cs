using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wait.Abstract;
using Wait.Database;
using Wait.Entities;
using Wait.Extensions;

namespace Wait.Features.Users.CreateUser;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Username,
    string Password,
    string ConfirmPassword,
    string Email
);

public record CreateUserResponse(string Status, string Message, string Username);

internal sealed class CreateUserHandler(AppDbContext dbContext)
{
    public async Task<CreateUserResponse> CreateUserAsync(
        CreateUserRequest request,
        IPasswordHasher<User> passwordHasher,
        CancellationToken ct)
    {
        if (await dbContext.User.AnyAsync(u => u.Username == request.Username, ct))
        {
            return new CreateUserResponse("error", "Username already exists", request.Username);
        }
        
        var user = request.ToEntity(passwordHasher);

        await dbContext.User.AddAsync(user, ct);

        try
        {
            await dbContext.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            return new CreateUserResponse("error", $"Failed to create user: {ex.Message}", request.Username);
        }

        return new CreateUserResponse("success", "User created successfully", user.Username);
    }
}

public sealed class CreateUser : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/v1/user/create", async (
            CreateUserRequest request,
            CreateUserHandler handler,
            IPasswordHasher<User> passwordHasher,
            CancellationToken ct) =>
        {
            try
            {
                var response = await handler.CreateUserAsync(request, passwordHasher, ct);

                return Results.Created(
                    $"/api/v1/user/{response.Username}",
                    response
                );
            }
            catch (InvalidOperationException ex)
            {
                return Results.BadRequest(
                    new CreateUserResponse("error", ex.Message, request.Username)
                );
            }
            catch (Exception ex)
            {
                return Results.Problem(
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "An unexpected error occurred while creating the user"
                );
            }
        })
        .WithValidation<CreateUserRequest>();
    }
}