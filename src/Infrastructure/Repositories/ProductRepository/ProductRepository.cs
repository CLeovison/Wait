using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Request.ProductRequest;
using Wait.Database;
using Wait.Entities;
using Wait.Extensions;

namespace Wait.Infrastructure.Repositories.ProductRepository;

public sealed class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<Product> CreateProductAsync(Product product, CancellationToken ct)
    {
        await dbContext.Product.AddAsync(product, ct);
        await dbContext.SaveChangesAsync(ct);
        return product;
    }

    public async Task<(List<Product>, int totalCount)> GetPaginatedProductAsync(FilterProductRequest filter,
    int page, int pageSize, string sortBy,
    bool desc, string searchTerm, CancellationToken ct)
    {
        var productQuery = dbContext.Product.Search(searchTerm).Filter(filter).Sort(sortBy, desc);
        var totalCount = await productQuery.CountAsync(ct);
        var paginatedProduct = await productQuery.Skip(page).AsNoTracking().ToListAsync();

        return (paginatedProduct, totalCount);

    }

    public async Task<Product?> GetProductByIdAsync(int id, CancellationToken ct)
    {
        return await dbContext.Product.FindAsync(id, ct);
    }

    public async Task<bool> DeleteProductAsync(Product product, CancellationToken ct)
    {
        dbContext.Product.Remove(product);
        await dbContext.SaveChangesAsync(ct);
        return product is not null;
    }

    public async Task<Product?> UpdateProductAsync(Product product, CancellationToken ct)
    {
        dbContext.Product.Update(product);
        await dbContext.SaveChangesAsync(ct);
        return product;
    }
}