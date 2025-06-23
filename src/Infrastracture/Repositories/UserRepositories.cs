using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Request.UserRequest;
using Wait.Database;
using Wait.Domain.Entities;
using Wait.Extensions;

namespace Wait.Infrastracture.Repositories;


public sealed class UserRepositories(IDbContextFactory<AppDbContext> dbContextFactory) : IUserRepositories
{
    public async Task<bool> CreateUserAsync(Users users)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var createUser = await dbContext.Set<Users>().AddAsync(users);
        await dbContext.SaveChangesAsync();
        return createUser is not null;
    }

    public async Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        return await dbContext.User.ToListAsync(ct);
    }

    public async Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        return await dbContext.User.FindAsync(id, ct);
    }

    public async Task<(List<Users>, int totalCount)> PaginatedUserAsync(FilterUserRequest filters,
     string? searchTerm,
     int skip,
     int take,
     string sortBy,
     bool desc,
     CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var filteredUsers = dbContext.User.Filter(filters).Search(searchTerm).Sort(sortBy, desc);

        var paginatedUser = await filteredUsers.Skip(skip).Take(take).ToListAsync(ct);

        var totalCount = await filteredUsers.CountAsync(ct);

        return (paginatedUser, totalCount);
    }

    public async Task<Users?> UpdateUserAsync(Users users, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        dbContext.Set<Users>().Update(users);

        await dbContext.SaveChangesAsync(ct);

        return users;
    }
    public async Task<bool> DeleteUserAsync(Users users)
    {
        using var dbContext = dbContextFactory.CreateDbContext();
        var deleteUser = dbContext.Set<Users>().Remove(users);
        await dbContext.SaveChangesAsync();

        return deleteUser is not null;

    }

}