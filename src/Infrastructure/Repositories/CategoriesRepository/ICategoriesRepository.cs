using Wait.Contracts.Request.CategoriesRequest;
using Wait.Domain.Entities;

namespace Wait.Infrastructure.Repositories.CategoriesRepository;


public interface ICategoriesRepository
{
    Task<Category> CreateCategoriesAsync(Category category, CancellationToken ct);
    Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken ct);
    Task<(List<Category>, int totalCount)> GetAllCategoryAsync(FilterCategoriesRequest req,
    string searchTerm, string sortBy, bool desc, int skip,
     int take, CancellationToken ct);
    Task<Category> UpdateCategoryAsync(Category category, CancellationToken ct);
    Task<bool> DeleteCategoryAsync(Category category, CancellationToken ct);
}