using Wait.Abstract;
using Wait.Contracts.Request.AuthRequest;
using Wait.Contracts.Response;
using Wait.Services.AuthService;

namespace Wait.Endpoint.AuthEndpoint;


public sealed class RefreshtTokenEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/refresh-token", async (RefreshTokenRequest request, IAuthService authService, CancellationToken ct) =>
        {
            var requestRefresh = new AuthResponse
            {
                RefreshToken = request.RefreshToken
            };

            return await authService.RefreshTokenAsync(requestRefresh);
        });
    }
}