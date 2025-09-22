
using Wait.Contracts.Request.CategoriesRequest;
using Wait.Contracts.Request.Common;
using Wait.Domain.Entities;
using Wait.Helper;
using Wait.Infrastructure.Mapping;
using Wait.Infrastructure.Repositories.CategoriesRepository;

namespace Wait.Services.Categories;

public sealed class CategoriesService(ICategoriesRepository categoriesRepository) : ICategoriesService
{
    public async Task<CategoryDto?> CreateCategoryAsync(CategoryDto category, CancellationToken ct)
    {
        try
        {
            var categoryMap = category.ToCreate();

            var categoryName = await categoriesRepository.GetCategoryNameAsync(category.CategoryName, ct);
            if (categoryName is null)
            {
                categoryName = await categoriesRepository.CreateCategoriesAsync(categoryMap, ct);
            }

            var categoryEntity = categoryName?.ToDto();

            return categoryEntity;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Exception", ex);
        }

    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken ct)
    {
        try
        {
            var category = await categoriesRepository.GetCategoryByIdAsync(id, ct);

            if (category is null)
            {
                throw new ArgumentNullException($"The User ${id} does not exist");
            }

            var mapCategory = category.ToDto();

            return mapCategory;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("There is an error", ex);
        }

    }
    public async Task<(List<CategoryDto>, int totalCount)> GetAllCategoryAsync(FilterCategoriesRequest filter, PaginatedRequest req, CancellationToken ct)
    {
        var sortCategory = SortDefaults.GetDefaultSortField("CategoryName");
        var pagination = PaginationProcessor.Create(req, sortCategory);

        var (category, totalCount) = await categoriesRepository.GetAllCategoryAsync(filter,
            req.SearchTerm, pagination.EffectiveSortBy, pagination.Descending, pagination.Take,
          pagination.Skip, ct);

        var categoryMap = category.Select(c => c.ToDto()).ToList();

        return (categoryMap, totalCount);

    }
    public async Task<CategoryDto> UpdateCategoryAsync(Guid id, CategoryDto category, CancellationToken ct)
    {
        var existingCategory = await categoriesRepository.GetCategoryByIdAsync(id, ct);

        if (existingCategory is null)
        {
            throw new ArgumentNullException();
        }

        try
        {
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.CategoryDescription = category.CategoryDescription;
            existingCategory.ImageUrl = category.ImageUrl;

            await categoriesRepository.UpdateCategoryAsync(existingCategory, ct);
            return category;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Error", ex);
        }
    }

    public async Task<bool> DeleteCategoryAsync(Guid id, CancellationToken ct)
    {
        var existingCategory = await categoriesRepository.GetCategoryByIdAsync(id, ct);

        if (existingCategory is null)
        {
            throw new ArgumentException("The User does not exist");
        }

        var removeUser = await categoriesRepository.DeleteCategoryAsync(existingCategory, ct);
        return removeUser;
    }
}