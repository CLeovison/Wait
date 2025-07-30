using Wait.Abstract;
using Wait.Services.AuthService;

namespace Wait.Endpoint.AuthEndpoint;

public record Request(string request);
public sealed class RefreshtTokenEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/refresh-token", async (Request request, IAuthService authService, CancellationToken ct) =>
        {
            return await authService.GetUserRefreshTokenAsync(request.request, ct);
        });
    }
}