using Wait.Entities;

namespace Wait.Infrastructure.Repositories.ProductRepository;


public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product, CancellationToken ct);
}