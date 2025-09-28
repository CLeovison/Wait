using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Data;
using Wait.Contracts.Request.CategoriesRequest;
using Wait.Database;
using Wait.Domain.Entities;
using Wait.Entities;
using Wait.Extensions;

namespace Wait.Infrastructure.Repositories.CategoriesRepository;


public sealed class CategoriesRepository(AppDbContext dbContext) : ICategoriesRepository
{
    public async Task<Category> CreateCategoriesAsync(Category category, CancellationToken ct)
    {
        await dbContext.Category.AddAsync(category, ct);
        await dbContext.SaveChangesAsync(ct);

        return category;
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken ct)
    {
        return await dbContext.Category.FindAsync(id);
    }

    public async Task<(List<CategoryDto>, int totalCount)> GetAllCategoryAsync(FilterCategoriesRequest req, string? searchTerm,
     int skip,
     int take,
     string sortBy,
     bool desc,
     CancellationToken ct)
    {
        var filteredCategory = dbContext.Category.Search(searchTerm).Filter(req).Sort(sortBy, desc);

        var totalCount = await filteredCategory.CountAsync(ct);
        var paginatedCategory = await filteredCategory.Skip(skip).Take(take).Select(c => new CategoryDto
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            Products = c.Products.Select(p => new ProductDtoTest
            {
                ProductName = p.ProductName,
                Description = p.Description,
                Color = p.Color
            }).ToList()
        }).ToListAsync(ct);
        return (paginatedCategory, totalCount);
    }

    public async Task<Category?> GetCategoryNameAsync(string categoryName, CancellationToken ct)
    {
        return await dbContext.Category.FirstOrDefaultAsync(c => c.CategoryName.ToLower() == categoryName.ToLower().Trim(), ct);
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