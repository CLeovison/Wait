using Wait.Contracts.Data;

namespace Wait.Services.ProductServices;

public interface IProductService
{
    Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken ct);

}