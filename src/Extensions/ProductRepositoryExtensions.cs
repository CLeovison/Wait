using Wait.Entities;

namespace Wait.Extensions;

public static class ProductRepositoryExtensions
{
    public static IQueryable<Product> Search(this IQueryable<Product> search, string? searchTerm)
    {
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            return search;
        }

        var lowerCaseTerm = searchTerm?.Trim().ToLower();

        return search.Where(x => x.ProductName.ToLower().Contains(lowerCaseTerm ?? string.Empty) || x.Price.ToString().Contains(searchTerm ?? string.Empty));
    }
}