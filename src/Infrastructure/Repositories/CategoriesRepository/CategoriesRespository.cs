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
}