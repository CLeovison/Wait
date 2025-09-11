using Wait.Contracts.Request.CategoriesRequest;
using Wait.Domain.Entities;

namespace Wait.Services.Categories;


public interface ICategoriesService
{
    Task<CategoryDto> CreateCategoryAsync(CategoryDto category, CancellationToken ct);
    Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken ct);

    Task<(List<CategoryDto>, int totalCount)> GetAllCategoryAsync(FilterCategoriesRequest request, string? searchTerm, int skip, int take, string desc, string sortBy);
    Task<CategoryDto> UpdateCategoryAsync(Guid id, CategoryDto category, CancellationToken ct);
    Task<bool> DeleteCategoryAsync(Guid id, CancellationToken ct);
}