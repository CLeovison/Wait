using Wait.Abstract;
using Wait.Contracts.Request.AuthRequest;
using Wait.Services.AuthService;

namespace Wait.Endpoint.AuthEndpoint;


public sealed class LoginEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/login", async (LoginUserRequest req, IAuthService authService, CancellationToken ct) =>
        {
            return await authService.LoginUserAsync(req.Username, req.Password, ct);


        });
    }
}