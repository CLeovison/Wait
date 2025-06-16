using System.Linq.Expressions;
using Wait.Contracts.Request.Common;
using Wait.Domain.Entities;

namespace Wait.Extensions;


public static class UserRepositoryExtensions
{
    public static IQueryable<Users> Filter(this IQueryable<Users> filter)
    {

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
        if (req.SortBy?.ToLower() == "desc")
        {
            sort = sort.OrderByDescending(UserSortProperty(req));
        }
        else
        {
            sort = sort.OrderBy(UserSortProperty(req));
        }
        return sort;
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