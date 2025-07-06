using Wait.Abstract;
using Wait.Services.UserServices;
using Wait.Infrastracture.Mapping;

namespace Wait.Endpoint.UserEndpoint;


public sealed class GetAllUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("/api/users", async (IUserServices userServices,CancellationToken ct) =>
        {
            var allUser = await userServices.GetAllUserAsync(ct);
            var modifiedUser = allUser.ToGetAllUserResponse();
            return modifiedUser;
        });
    }
}