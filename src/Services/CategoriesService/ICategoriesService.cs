using Wait.Contracts.Request.CategoriesRequest;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Response;
using Wait.Domain.Entities;

namespace Wait.Services.Categories;


public interface ICategoriesService
{
    Task<CategoryDto?> CreateCategoryAsync(CategoryDto category, CancellationToken ct);
    Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken ct);

    Task<PaginatedResponse<CategoryDto>> GetAllCategoryAsync(FilterCategoriesRequest filter, PaginatedRequest req, CancellationToken ct);
    Task<CategoryDto> UpdateCategoryAsync(Guid id, CategoryDto category, CancellationToken ct);
    Task<bool> DeleteCategoryAsync(Guid id, CancellationToken ct);
}