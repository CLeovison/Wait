using Wait.Domain.Entities;

namespace Wait.Services.Categories;


public interface ICategoriesService
{
    Task<CategoryDto> CreateCategoryAsync(Category category, CancellationToken ct);
}