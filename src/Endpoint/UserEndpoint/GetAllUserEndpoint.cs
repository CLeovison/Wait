using Wait.Abstract;
using Wait.Services.UserServices;
using Wait.Mapping;


namespace Wait.Endpoint.UserEndpoint;


public sealed class GetAllUserEndpoint(IUserServices userServices) : IEndpoint
{

    public void Endpoint(IEndpointRouteBuilder app)
    {

        app.MapGet("/api/users", async (CancellationToken ct) =>
        {
            var allUser = await userServices.GetAllUserAsync(ct);
            var modifiedUser = allUser.ToGetAllUserResponse();
            return modifiedUser;
        });
    }
}