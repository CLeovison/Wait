namespace Wait.Extensions;


public static class HttpContextAccessorExtensions
{

    public static void WriteTokenAsHttpOnlyCookie(this IHttpContextAccessor accessor,
    string cookieName,
    string token,
    DateTime expiration,
    string path = "/")
    {
        accessor?.HttpContext?.Response.Cookies.Append(cookieName, token,
        new CookieOptions
        {
            HttpOnly = true,
            Expires = expiration,
            IsEssential = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = path
        });
    }
}