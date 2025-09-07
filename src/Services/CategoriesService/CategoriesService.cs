
using Wait.Domain.Entities;
using Wait.Infrastructure.Mapping;
using Wait.Infrastructure.Repositories.CategoriesRepository;

namespace Wait.Services.Categories;

public sealed class CategoriesService(ICategoriesRepository categoriesRepository) : ICategoriesService
{
    public async Task<CategoryDto> CreateCategoryAsync(Category category, CancellationToken ct)
    {
        var categoryRequest = await categoriesRepository.CreateCategoriesAsync(category, ct);
        var categoryMap = categoryRequest.ToDto();
        return categoryMap;
    }
}