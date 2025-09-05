using Wait.Contracts.Data;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Request.ProductRequest;
using Wait.Contracts.Response;
using Wait.Helper;
using Wait.Infrastructure.Mapping;
using Wait.Infrastructure.Repositories.ProductRepository;

namespace Wait.Services.ProductServices;


public sealed class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken ct)
    {
        var productDto = product.ToCreate();
        var request = await productRepository.CreateProductAsync(productDto, ct);
        var response = request.ToDto();
        return response;
    }

    public async Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken ct)
    {
        var request = await productRepository.GetProductByIdAsync(id, ct);

        if (request is null)
        {
            Results.NotFound();
        }

        var productResponse = request?.ToDto();

        return productResponse;
    }

    public async Task<PaginatedResponse<ProductDto>> GetPaginatedProductAsync(FilterProductRequest filters, PaginatedRequest req, CancellationToken ct)
    {
        var defaultSort = SortDefaults.GetDefaultSortField("ProductName");
        var pagination = PaginationProcessor.Create(req, defaultSort);

        var (products, totalCount) = await productRepository.GetPaginatedProductAsync(
            filters,
            pagination.Skip, 
            pagination.Take, 
            pagination.EffectiveSortBy ?? defaultSort,
            pagination.Descending, 
            req.SearchTerm , 
            ct
        );

        var productDtos = products.Select(p => p.ToDto()).ToList();
        return new PaginatedResponse<ProductDto>(productDtos, pagination.Page, pagination.PageSize, totalCount);
    }
}