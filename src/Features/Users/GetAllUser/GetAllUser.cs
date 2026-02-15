using Microsoft.EntityFrameworkCore;
using Wait.Abstract;
using Wait.Database;
using Wait.Entities;

namespace Wait.Features.Users.GetAlluser;

internal sealed class GetAllUserHandler(AppDbContext dbContext)
{
    public async Task<IReadOnlyList<User>> GetAllUserAsync(int pageNumber,
    int pageSize,
    string? searchTerm,
    UserFilter filter,
    CancellationToken ct)
    {
        var query = dbContext.User.AsNoTracking();

        var lowerCase = searchTerm?.Trim().ToLower();

        if (!string.IsNullOrWhiteSpace(lowerCase))
        {
            query = query.Where(x => x.Username.Contains(lowerCase) || x.FirstName.Contains(lowerCase));
        }
        if (!string.IsNullOrWhiteSpace(filter.FirstName))
        {
            query = query.Where(x => x.FirstName.Contains(filter.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(filter.Username))
        {
            query = query.Where(x => x.Username.Contains(filter.Username));
        }

        return await query
        .OrderBy(x => x.Username)
        .ThenBy(x => x.UserId)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync(ct);
    }

}


public sealed class GetAllUser : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/users", async (
            GetAllUserHandler handler,
            [AsParameters] UserFilter filter,
            CancellationToken ct,
            int pageNumber = 1,
            int pageSize = 10,
            string? searchTerm = null) =>
        {
            var users = await handler.GetAllUserAsync(
                pageNumber,
                pageSize,
                searchTerm,
                filter,
                ct);

            return Results.Ok(users);
        });
    }
}