using Wait.Contracts.Data;
using Wait.Contracts.Request.Common;
using Wait.Contracts.Request.ProductRequest;
using Wait.Contracts.Response;

namespace Wait.Services.ProductServices;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken ct);
    Task<ProductDto?> GetProductByIdAsync(Guid id, CancellationToken ct);
    Task<PaginatedResponse<ProductDto>> GetPaginatedProductAsync( PaginatedRequest req,FilterProductRequest filters, CancellationToken ct);
    // Task<bool> DeleteProductAsync(int id, CancellationToken ct);

}