using Wait.Domain.Entities;

namespace Wait.Services.Categories;


public interface ICategoriesService
{
    Task<CategoryDto> CreateCategoryAsync(CategoryDto category, CancellationToken ct);
    Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken ct);
}