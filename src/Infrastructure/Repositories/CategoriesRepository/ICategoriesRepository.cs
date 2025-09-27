using Wait.Contracts.Request.CategoriesRequest;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories.CategoriesRepository;


public interface ICategoriesRepository
{
    Task<Category> CreateCategoriesAsync(Category category, CancellationToken ct);
    Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken ct);
    Task<(List<CategoryDto>, int totalCount)> GetAllCategoryAsync(FilterCategoriesRequest req,
 string? searchTerm,
     int skip,
     int take,
     string sortBy,
     bool desc,
     CancellationToken ct);
    Task<Category?> GetCategoryNameAsync(string categoryName, CancellationToken ct);
    Task<Category> UpdateCategoryAsync(Category category, CancellationToken ct);
    Task<bool> DeleteCategoryAsync(Category category, CancellationToken ct);

}