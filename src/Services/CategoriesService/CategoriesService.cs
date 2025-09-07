
using Wait.Domain.Entities;
using Wait.Infrastructure.Mapping;
using Wait.Infrastructure.Repositories.CategoriesRepository;

namespace Wait.Services.Categories;

public sealed class CategoriesService(ICategoriesRepository categoriesRepository) : ICategoriesService
{
    public async Task<CategoryDto> CreateCategoryAsync(CategoryDto category, CancellationToken ct)
    {
        var categoryMap = category.ToCreate();
        var categoryRequest = await categoriesRepository.CreateCategoriesAsync(categoryMap, ct);
        var categoryEntity = categoryRequest.ToDto();
        return categoryEntity;
    }
}