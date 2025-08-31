using System.Linq.Expressions;

namespace Wait.Extensions;


public static class QueryableExtensions
{

    public static IQueryable<T> Filter<T>(this IQueryable<T> filter)
    {
        return filter;
    }
    public static IQueryable<T> Search<T>(this IQueryable<T> search,
    Expression<Func<T, string>> expression,
    string? searchTerm)
    {

        if (expression is null)
        {
            throw new ArgumentException(nameof(expression));
        }

        if (search is null)
        {
            throw new ArgumentException(nameof(search));
        }

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            throw new ArgumentNullException($"{searchTerm} was not existing");
        }

        var lowerSearchTerm = searchTerm.ToLower().Trim();

        var parameter = expression.Parameters[0];
        var parameterAccess = expression.Body;

        var notNull = Expression.NotEqual(parameterAccess, Expression.Constant(null, typeof(string)));
        var toLowerCall = Expression.Call(parameterAccess, nameof(string.ToLower), Type.EmptyTypes);
        var containsCall = Expression.Call(toLowerCall, nameof(string.Contains), Type.EmptyTypes, Expression.Constant(searchTerm));


        
    }

    public static IQueryable<T> Sort<T>(this IQueryable<T> sort)
    {
        return sort;
    }
}