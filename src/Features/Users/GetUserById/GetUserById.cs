using Wait.Database;

namespace Wait.Features.Users.GetUserById;

internal sealed class GetUserByIdHandler(AppDbContext dbContext)
{

    public async Task<bool> Handler(Guid id, CancellationToken ct)
    {
        var users = await dbContext.User.FindAsync(id, ct);

        if (users is null)
        {
            throw new ArgumentNullException($"The user {users} doesn't exist");
        }

        return users is not null;
    }
}