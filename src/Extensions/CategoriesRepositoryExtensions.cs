using System.Linq.Expressions;
using Wait.Contracts.Request.CategoriesRequest;
using Wait.Domain.Entities;

namespace Wait.Extensions;

public static class CategoriesRepositoryExtensions
{

    public static IQueryable<Category> Search(this IQueryable<Category> search, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return search;
        }

        return search.Where(x => x.CategoryName.ToLower().Contains(searchTerm.ToLower()));
    }

    public static IQueryable<Category> Filter(this IQueryable<Category> filter, FilterCategoriesRequest req)
    {
        if (req is null) return filter;

        if (!string.IsNullOrWhiteSpace(req.CategoryName))
        {
            filter = filter.Where(x => x.CategoryName.Contains(req.CategoryName));
        }

        return filter;
    }

    public static IQueryable<Category> Sort(this IQueryable<Category> query, string sortBy, bool desc)
    {
        Expression<Func<Category, object>> keySelector = sortBy.ToLower() switch
        {
            "categoryName" => category => category.CategoryName,
            _ => category => category.CategoryId


        };
        return desc ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
    }
}