using System.Linq.Expressions;
using Wait.Contracts.Request.UserRequest;
using Wait.Domain.Entities;
using Wait.Entities;

namespace Wait.Extensions;


public static class ProductRepositoryExtensions
{
    public static IQueryable<Product> Filter(this IQueryable<Product> filter, FilterUserRequest req)
    {
        if (req is null) return filter;

        if (!string.IsNullOrWhiteSpace(req.FirstName))
        {
            filter = filter.Where(x => x.ProductName.Contains(req.FirstName));
        }
        if (!string.IsNullOrWhiteSpace(req.LastName))
        {
            filter = filter.Where(x => x.ProductSize.Contains(req.LastName));
        }

        if (req.CreatedAt.HasValue)
        {
            filter = filter.Where(x => x.CreatedAt > req.CreatedAt);
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

        return search.Where(x => x.ProductName.ToLower().Contains(lowerCaseTerm) || x.ProductSize.ToLower().Contains(lowerCaseTerm));

    }

    public static IQueryable<Product> Sort(this IQueryable<Product> query, string sortBy, bool descending)
    {
        Expression<Func<Product, object>> keySelector = sortBy.ToLower() switch
        {
            "productname" => product => product.ProductName,
            "price" => product => product.Price,
            _ => product => product.ProductId
        };

        return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
    }

}