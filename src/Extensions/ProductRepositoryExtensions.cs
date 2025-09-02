using System.Linq.Expressions;
using Wait.Contracts.Request.ProductRequest;
using Wait.Entities;

namespace Wait.Extensions;


public static class ProductRepositoryExtensions
{
    public static IQueryable<Product> Filter(this IQueryable<Product> filter, FilterProductRequest req)
    {
        if (req is null) return filter;

        if (!string.IsNullOrWhiteSpace(req.ProductName))
        {
            filter = filter.Where(x => x.ProductName.Contains(req.ProductName));
        }
        if (!string.IsNullOrWhiteSpace(req.Size))
        {
            filter = filter.Where(x => x.Size.Contains(req.Size));
        }

        if (!string.IsNullOrWhiteSpace(req.Color))
        {
            filter = filter.Where(x => x.Color.Contains(req.Color));
        }

        return filter;

    }
    public static IQueryable<Product> Search(this IQueryable<Product> search, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return search;
        }

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return search.Where(x => x.ProductName.ToLower().Contains(lowerCaseTerm) || x.Size.ToLower().Contains(lowerCaseTerm));

    }

    public static IQueryable<Product> Sort(this IQueryable<Product> query, string sortBy, bool descending)
    {
        Expression<Func<Product, object>> keySelector = sortBy.ToLower() switch
        {
            "productname" => product => product.ProductName,
            "price" => product => product.Price,
            "color" => product => product.Color,
            _ => product => product.ProductId
        };

        return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
    }

}