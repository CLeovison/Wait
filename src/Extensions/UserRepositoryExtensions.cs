using System.Linq.Expressions;
using Wait.Contracts.Request.Common;
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

        return search.Where(x => x.FirstName.Contains(lowerCaseTerm) || x.LastName.Contains(lowerCaseTerm));
    }

    public static IQueryable<Users> Sort(this IQueryable<Users> sort, PaginatedRequest req)
    {

        var property = UserSortProperty(req);
        var isDecending = req.SortDirection?.ToLower() == "desc";

        return isDecending ? sort.OrderByDescending(property) : sort.OrderBy(property);
    }


    private static Expression<Func<Users, object>> UserSortProperty(PaginatedRequest req)
    {

        return req.SortBy?.ToLower() switch
        {

            "firstname" => users => users.FirstName,
            "lastname" => users => users.LastName,
            "email" => users => users.Email,
            _ => users => users.UserId
        };
    }
}