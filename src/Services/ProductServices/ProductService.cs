using Wait.Contracts.Data;
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
}