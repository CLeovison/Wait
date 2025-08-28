namespace Wait.Extensions;


public static class QueryableExtensions
{

    public static IQueryable<T> Filter<T>(this IQueryable<T> filter)
    {
        return filter;
    }
    public static IQueryable<T> Search<T>(this IQueryable<T> search)
    {
        return search;
    }
}