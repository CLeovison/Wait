using Wait.Contracts.Data;
using Wait.Contracts.Response;
using Wait.Entities;

namespace Wait.Infrastructure.Repositories.ProductRepository;


public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product, CancellationToken ct);
    Task<(List<Product>, int totalCount)> GetPaginatedProductAsync(int page, int pageSize, string sortBy,
    bool desc, string searchTerm, CancellationToken ct);
    Task<Product> GetProductByIdAsync(int id, CancellationToken ct);
    Task<bool> DeleteProductAsync(int id, CancellationToken ct);
    Task<Product?> UpdateProductAsync(int id, CancellationToken ct);
}