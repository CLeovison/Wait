using Wait.Contracts.Data;
using Wait.Contracts.Request.Common;

namespace Wait.Services.ProductServices;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken ct);
    Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken ct);

}