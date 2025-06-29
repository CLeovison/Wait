using Wait.Abstract;
using Wait.Contracts.Request.UserRequest;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;


public sealed class LoginUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/login", async (LoginUserRequest req) =>
        {


            var userLogin = await userServices.LoginUserAsync(req.Username, req.Password);

            return userLogin;
        }
    );
    }
}