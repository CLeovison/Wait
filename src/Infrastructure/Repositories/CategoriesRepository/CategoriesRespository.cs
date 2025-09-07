using Wait.Database;
using Wait.Domain.Entities;
using Wait.Infrastructure.Repositories.CategoriesRepository;

namespace ait.Infrastructure.Repositories.CategoriesRepository;


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

    public async Task<(List<Category>, int totalCount)> GetAllCategoryAsync(string? searchTerm, string sortBy, bool desc,  int skip,
     int take, CancellationToken ct)
    {

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