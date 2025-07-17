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
        var createUser = await dbContext.AddAsync(users, ct);
        await dbContext.SaveChangesAsync();
        return createUser.Entity;
    }

    public async Task<IEnumerable<Users>> GetAllUserAsync(CancellationToken ct)
    {
 
        return await dbContext.User.ToListAsync(ct);
    }

    public async Task<Users?> GetUserByIdAsync(Guid id, CancellationToken ct)
    {
   
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


        var filteredUsers = dbContext.User.Filter(filters).Search(searchTerm).Sort(sortBy, desc);

        var paginatedUser = await filteredUsers.Skip(skip).Take(take).ToListAsync(ct);

        var totalCount = await filteredUsers.CountAsync(ct);

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
       
        var deleteUser = dbContext.Remove(users);
        await dbContext.SaveChangesAsync(ct);

        return deleteUser is not null;
    }

    public async Task<Users?> GetUserByUsernameAsync(string username,CancellationToken ct)
    {
        var selectUser = await dbContext.User.FirstOrDefaultAsync(x => x.Username == username,ct);
        return selectUser;
    }

}