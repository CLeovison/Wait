using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Request.UserRequest;
using Wait.Database;
using Wait.Domain.Entities;
using Wait.Extensions;

namespace Wait.Infrastructure.Repositories.UserRepository;

public sealed class UserRepositories(AppDbContext dbContext) : IUserRepositories
{
    public async Task<Users> CreateUserAsync(Users users, CancellationToken ct)
    {
        await dbContext.User.AddAsync(users, ct);

        await dbContext.SaveChangesAsync();

        return users;
    }

    public async Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.User.FindAsync(id, ct);
    }

    public async Task<(List<Users>, int totalCount)> GetPaginatedUserAsync(FilterUserRequest filters,
     string? searchTerm,
     int skip,
     int take,
     string sortBy,
     bool desc,
     CancellationToken ct)
    {

        var filteredUsers = dbContext.User.Filter(filters).Search(searchTerm).Sort(sortBy, desc);

        var totalCount = await filteredUsers.CountAsync(ct);

        var paginatedUser = await filteredUsers.Skip(skip).Take(take).ToListAsync(ct);

        return (paginatedUser, totalCount);
    }

    public async Task<Users?> UpdateUserAsync(Users users, CancellationToken ct)
    {
        dbContext.Update(users);

        await dbContext.SaveChangesAsync(ct);

        return users;
    }
    public async Task<bool> DeleteUserAsync(Users users, CancellationToken ct)
    {
        dbContext.User.Remove(users);

        await dbContext.SaveChangesAsync(ct);

        return users is not null;
    }

    public async Task<Users?> GetUserByUsernameAsync(string username, CancellationToken ct)
    {
        var selectUser = await dbContext.User.FirstOrDefaultAsync(x => x.Username == username, ct);

        return selectUser;
    }
    public async Task<Users?> GetUserByEmailAsync(string email, CancellationToken ct)
    {
        var userEmail = await dbContext.User.FirstOrDefaultAsync(x => x.Email == email, ct);

        return userEmail;
    }

}