using Microsoft.EntityFrameworkCore;
using Wait.Abstract;
using Wait.Database;


namespace Wait.Features.Users.GetUserById;

internal sealed class GetUserByIdHandler(AppDbContext dbContext)
{
    public async Task<GetUserByIdResponse> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
        var users = await dbContext.User.Where(x => x.UserId == id).Select(x => new GetUserByIdResponse
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
            Birthday = x.Birthday,
            Username = x.Username,
            Email = x.Email,
            CreatedAt = x.CreatedAt,
            ModifiedAt = x.ModifiedAt,
            IsVerifiedEmail = x.IsVerifiedEmail,
            IsDeleted = x.IsDeleted

        }).FirstOrDefaultAsync(ct);

        if (users is null)
        {
            throw new ArgumentNullException($"The user {users} doesn't exist");
        }

        return users;
    }
}

public sealed class GetUserByIdEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/users/{id}", async (Guid id, GetUserByIdHandler handler, CancellationToken ct) =>
        {
            var users = await handler.GetUserByIdAsync(id, ct);

            return Results.Ok(users);
        });
    }
}