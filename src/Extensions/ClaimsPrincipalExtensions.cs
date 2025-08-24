using System.Security.Claims;

namespace Wait.Extensions;


public static class ClaimsPrincipalExtensions
{
    public static string? GetUsername(this ClaimsPrincipal principal)
    {
        return principal.FindFirst(ClaimTypes.Name)?.Value
        ?? principal.FindFirst("username")?.Value
        ?? principal.FindFirst("sub")?.Value;
    }   
}