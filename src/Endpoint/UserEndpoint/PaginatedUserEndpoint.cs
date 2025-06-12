using Wait.Abstract;


using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;


public sealed class PaginatedUserEndpoint(IUserServices userServices) : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {

    }
}