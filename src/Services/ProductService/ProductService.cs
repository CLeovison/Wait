using Npgsql;
using Wait.Contracts.Data;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Request.ProductRequest;
using Wait.Contracts.Response;
using Wait.Domain.Entities;
using Wait.Helper;
using Wait.Infrastructure.Mapping;
using Wait.Infrastructure.Repositories.CategoriesRepository;
using Wait.Infrastructure.Repositories.ProductRepository;

namespace Wait.Services.ProductServices;


public sealed class ProductService(IProductRepository productRepository, ICategoriesRepository categoriesRepository) : IProductService
{
    public async Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken ct)
    {
        var normalizedCategory = product.CategoryName.Trim();
        var category = await categoriesRepository.GetCategoryNameAsync(normalizedCategory, ct);

        if (category == null)
        {
            throw new ArgumentNullException("The Category does not exist, please add this shit");
        }

        var createProduct = product.ToCreate(category.CategoryId);
        var request = await productRepository.CreateProductAsync(createProduct, ct);
        return request.ToDto();
    }
    public async Task<ProductDto?> GetProductByIdAsync(Guid id, CancellationToken ct)
    {
        var request = await productRepository.GetProductByIdAsync(id, ct);

        if (request is null)
        {
            Results.NotFound();
        }

        var productResponse = request?.ToDto();

        return productResponse;
    }

    public async Task<PaginatedResponse<ProductDto>> GetPaginatedProductAsync(PaginatedRequest req, FilterProductRequest filters, CancellationToken ct)
    {
        var defaultSort = SortDefaults.GetDefaultSortField("ProductName");
        var pagination = PaginationProcessor.Create(req, defaultSort);

        var (products, totalCount) = await productRepository.GetPaginatedProductAsync(
            filters,
            pagination.Skip,
            pagination.Take,
            pagination.EffectiveSortBy ?? defaultSort,
            pagination.Descending,
            req.SearchTerm,
            ct
        );

        var productDtos = products.Select(p => p.ToDto()).ToList();
        return new PaginatedResponse<ProductDto>(productDtos, pagination.Page, pagination.PageSize, totalCount);
    }
}