using Wait.Abstract;

using Wait.Services.AuthService;

namespace Wait.Endpoint.AuthEndpoint;


public sealed class RefreshTokenEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/refresh-token", async (HttpContext context, IAuthService authService) =>
        {
            var expiredAccessToken = context.Request.Cookies["accessToken"];
            var refreshToken = context.Request.Cookies["refreshToken"];

            if (string.IsNullOrWhiteSpace(expiredAccessToken) || string.IsNullOrWhiteSpace(refreshToken))
                throw new ArgumentException("Access token is required in the Authorization header.");

            await authService.RefreshTokenAsync(expiredAccessToken, refreshToken);


            return TypedResults.NoContent();
        });
    }
}