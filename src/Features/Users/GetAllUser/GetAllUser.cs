using Microsoft.EntityFrameworkCore;
using Wait.Abstract;
using Wait.Database;
using Wait.Entities;

namespace Wait.Features.Users.GetAlluser;

internal sealed class GetAllUserHandler(AppDbContext dbContext)
{

    public async Task<User> GetAllUserAsync(int pageNumber, int pageSize, string search, User user, CancellationToken ct)
    {
        IQueryable<User> userQuery = dbContext.User;
        if (string.IsNullOrWhiteSpace(search))
        {
            return user;
        }

        var lowerCase = search.Trim().ToLower();

        var users = await userQuery
        .OrderBy(x => x.Username)
        .Skip(pageNumber)
        .Take(pageSize)
        .OrderByDescending(x => x.Username)
        .Where(x => x.FirstName.Contains(lowerCase) || x.Username.Contains(lowerCase))
        .ToListAsync(ct);



    }

}


public sealed class GetAllUser : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/user", async)
    }
}