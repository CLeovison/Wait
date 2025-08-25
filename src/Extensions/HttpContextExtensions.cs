namespace Wait.Extensions;


public static class HttpContextExtensions
{

    public static void WriteTokenAsHttpOnlyCookie(this IHttpContextAccessor httpContextAccessor,
    string cookieName,
    string token,
    DateTime expiration)
    {
        httpContextAccessor?.HttpContext?.Response.Cookies.Append(cookieName, token,
        new CookieOptions
        {
            HttpOnly = true,
            Expires = expiration,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });
    }
}