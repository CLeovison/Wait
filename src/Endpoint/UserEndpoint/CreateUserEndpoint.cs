using Wait.Abstract;

using Wait.Contracts.Request.UserRequest;
using Wait.UserServices.Services;
using Wait.Mapping;

namespace Wait.Endpoint.UserEndpoint;


public class CreateUserEndpoint(IUserServices userServices) : IEndpoint
{


    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/create", async (CreateUserRequest req) =>
        {
            var userDto = req.ToRequest();
            return await userServices.CreateUserAsync(userDto);
        });
    }
}