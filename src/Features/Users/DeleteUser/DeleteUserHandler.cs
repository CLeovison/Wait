using Microsoft.EntityFrameworkCore;
using Wait.Abstract;
using Wait.Database;

namespace Wait.Features.Users.DeleteUser;

internal sealed class DeleteUserHandler(AppDbContext dbContext)
{
    public async Task<bool> DeleteUserAsync(Guid id, CancellationToken ct)
    {
        var query = await dbContext.User.FindAsync(id, ct);

        if (query is null)
        {
            throw new ArgumentNullException($"{query} did not exist");
        }
        dbContext.User.Remove(query);
        await dbContext.SaveChangesAsync(ct);

        return query is not null;
    }
}


public sealed class DeleteUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/v1/users/{id}", async (Guid id, DeleteUserHandler handler, CancellationToken ct) =>
        {
            var query = await handler.DeleteUserAsync(id, ct);

            return Results.Ok(new { Message = "User has been deleted", UserId = id });
        });
    }
}