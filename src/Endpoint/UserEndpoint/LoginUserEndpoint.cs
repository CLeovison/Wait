using Wait.Abstract;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;


public sealed class LoginUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/login", async (string username, string password) =>
        {
            var userLogin = await userServices.LoginUserAsync(username, password);

            return userLogin;
        }
    );
    }
}