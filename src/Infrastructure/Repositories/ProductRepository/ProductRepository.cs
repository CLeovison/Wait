using Microsoft.EntityFrameworkCore;
using Wait.Database;
using Wait.Entities;

namespace Wait.Infrastructure.Repositories.ProductRepository;

public sealed class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateProductAsync(Product product, CancellationToken ct)
    {
        var request = await dbContext.AddAsync(product, ct);
        await dbContext.SaveChangesAsync(ct);

        return request.Entity;
    }
    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken ct)
    {
        return await dbContext.Product.FindAsync(id, ct);

    }
}