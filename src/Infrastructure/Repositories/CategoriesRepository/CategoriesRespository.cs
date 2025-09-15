using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Request.CategoriesRequest;
using Wait.Database;
using Wait.Domain.Entities;
using Wait.Extensions;

namespace Wait.Infrastructure.Repositories.CategoriesRepository;


public sealed class CategoriesRepository(AppDbContext dbContext) : ICategoriesRepository
{
    public async Task<Category> CreateCategoriesAsync(Category category, CancellationToken ct)
    {
        await dbContext.Category.AddAsync(category);
        await dbContext.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.Category.FindAsync(id);
    }

    public async Task<(List<Category>, int totalCount)> GetAllCategoryAsync(FilterCategoriesRequest req, string searchTerm, string sortBy, bool desc, int skip,
     int take, CancellationToken ct)
    {
        var filteredCategory = dbContext.Category.Search(searchTerm).Filter(req).Sort(sortBy, desc);

        var totalCount = await filteredCategory.CountAsync(ct);
        var paginatedUser = await filteredCategory.Skip(skip).Take(take).ToListAsync();
        return (paginatedUser, totalCount);
    }
    public async Task<Category> UpdateCategoryAsync(Category category, CancellationToken ct)
    {
        dbContext.Category.Update(category);
        await dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<bool> DeleteCategoryAsync(Category category, CancellationToken ct)
    {
        dbContext.Category.Remove(category);
        await dbContext.SaveChangesAsync();
        return category is not null;
    }
}