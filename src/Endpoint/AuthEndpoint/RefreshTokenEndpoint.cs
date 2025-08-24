using Wait.Abstract;
using Wait.Contracts.Request.AuthRequest;
using Wait.Contracts.Response;
using Wait.Services.AuthService;

namespace Wait.Endpoint.AuthEndpoint;


public sealed class RefreshTokenEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/refresh-token", async (HttpContext context, RefreshTokenRequest request, IAuthService authService, CancellationToken ct) =>
        {
            var expiredAccessToken = context.Request.Headers["Authorization"]
                .ToString()
                .Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(expiredAccessToken))
                throw new ArgumentException("Access token is required in the Authorization header.");

            return await authService.RefreshTokenAsync(expiredAccessToken, request.RefreshToken);
        });
    }
}