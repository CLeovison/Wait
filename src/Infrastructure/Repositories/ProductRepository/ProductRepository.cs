using Wait.Database;
using Wait.Entities;

namespace Wait.Infrastructure.Repositories.ProductRepository;

public sealed class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateProductAsync(Product product, CancellationToken ct)
    {
        await dbContext.Product.AddAsync(product, ct);
        await dbContext.SaveChangesAsync(ct);
        return product;
    }
    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken ct)
    {
        return await dbContext.Product.FindAsync(id, ct);
    }

    public async Task<bool> DeleteProductAsync(int id, CancellationToken ct)
    {
        var request = dbContext.Remove(id);
        await dbContext.SaveChangesAsync(ct);

        return request is not null;
    }

    public async Task<Product?> UpdateProductAsync(Product product, CancellationToken ct)
    {
         dbContext.Update(product);
        await dbContext.SaveChangesAsync(ct);
        return product;
    }
}