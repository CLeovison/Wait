using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Response;
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
    public async Task<IEnumerable<Users>> SearchUserAsync(string? searchTerm, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        return await dbContext.User
            .Where(x =>
                !string.IsNullOrEmpty(searchTerm) &&
                (x.FirstName.Contains(searchTerm) || x.LastName.Contains(searchTerm))
            )
            .ToListAsync(ct);
    }
    public async Task<PaginatedResponse<Users>> PaginatedUserAsync(PaginatedRequest req, CancellationToken ct)
    {
        using var dbContext = dbContextFactory.CreateDbContext();

        var totalCount = await dbContext.User.CountAsync(ct);

        var paginatedUser = await dbContext.User
        .Skip((req.Page - 1) * req.PageSize)
        .Search(req.SearchTerm)
        .Sort(req)
        .Take(req.PageSize)
        .ToListAsync(ct);

        return new PaginatedResponse<Users>(paginatedUser, req.Page, req.PageSize, totalCount);
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