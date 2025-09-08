
using Wait.Domain.Entities;
using Wait.Infrastructure.Mapping;
using Wait.Infrastructure.Repositories.CategoriesRepository;

namespace Wait.Services.Categories;

public sealed class CategoriesService(ICategoriesRepository categoriesRepository) : ICategoriesService
{
    public async Task<CategoryDto> CreateCategoryAsync(CategoryDto category, CancellationToken ct)
    {
        string message = $"The {category.CategoryName} was already exisiting";

        if (category is not null)
        {
            throw new InvalidOperationException(message);
        }
        try
        {
            var categoryMap = category?.ToCreate();
            var categoryRequest = await categoriesRepository.CreateCategoriesAsync(categoryMap, ct);
            var categoryEntity = categoryRequest.ToDto();
            return categoryEntity;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Exception", ex);
        }

    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken ct)
    {
        

    }
}