using System.Linq.Expressions;

namespace Wait.Extensions;


public static class QueryableExtensions
{

    public static IQueryable<T> Filter<T>(this IQueryable<T> filter, params Expression<Func<T, bool>>[] expressions)
    {

        if (filter is null)
        {
            throw new ArgumentNullException(nameof(filter));
        }

        if (expressions is not null)
        {
            foreach (var expression in expressions)
            {
                filter = filter.Where(expression);
            }
        }

        return filter;
    }


    public static IQueryable<T> Sort<T>(this IQueryable<T> sort)
    {
        return sort;
    }
}