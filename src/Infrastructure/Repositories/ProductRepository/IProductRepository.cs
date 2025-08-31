
using Wait.Entities;

namespace Wait.Infrastructure.Repositories.ProductRepository;


public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product, CancellationToken ct);
    Task<(List<Product>, int totalCount)> GetPaginatedProductAsync(int page, int pageSize, string sortBy,
    bool desc, string searchTerm, CancellationToken ct);
    Task<Product?> GetProductByIdAsync(int id, CancellationToken ct);
    Task<bool> DeleteProductAsync(Product product, CancellationToken ct);
    Task<Product?> UpdateProductAsync(Product product, CancellationToken ct);
}