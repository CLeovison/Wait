using System.Linq.Expressions;
using Wait.Contracts.Request.UserRequest;
using Wait.Domain.Entities;

namespace Wait.Extensions;


public static class UserRepositoryExtensions
{
    public static IQueryable<Users> Filter(this IQueryable<Users> filter, FilterUserRequest req)
    {
        if (req is null) return filter;
        if (!string.IsNullOrWhiteSpace(req.FirstName))
        {
            filter = filter.Where(x => x.FirstName.Contains(req.FirstName));
        }
        if (!string.IsNullOrWhiteSpace(req.LastName))
        {
            filter = filter.Where(x => x.LastName.Contains(req.LastName));
        }

        if (req.CreatedAt.HasValue)
        {
            filter = filter.Where(x => x.CreatedAt > req.CreatedAt);
        }

        return filter;

    }
    public static IQueryable<Users> Search(this IQueryable<Users> search, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return search;
        }

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return search.Where(x => x.FirstName.ToLower().Contains(lowerCaseTerm) || x.LastName.ToLower().Contains(lowerCaseTerm));

    }

    public static IQueryable<Users> Sort(this IQueryable<Users> query, string sortBy, bool descending)
    {
        Expression<Func<Users, object>> keySelector = sortBy.ToLower() switch
        {
            "firstname" => user => user.FirstName,
            "lastname" => user => user.LastName,
            "email" => user => user.Email,
            _ => user => user.UserId
        };

        return descending ? query.OrderByDescending(keySelector) : query.OrderBy(keySelector);
    }

}
