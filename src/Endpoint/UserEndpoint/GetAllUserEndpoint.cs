using Wait.Abstract;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;


public class GetAllUserEndpoint : IEndpoint
{
    private readonly IUserServices _userServices;
    public GetAllUserEndpoint(IUserServices userServices)
    {
        _userServices = userServices;
    }
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", async (CancellationToken ct) =>
        {
            var getAllUser = await _userServices.GetAllUserAsync(ct);
            return getAllUser;
        });
    }
}