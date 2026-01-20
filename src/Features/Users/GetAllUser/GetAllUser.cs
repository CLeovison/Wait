using Wait.Abstract;
using Wait.Database;

namespace Wait.Features.Users.GetAlluser;

internal sealed class GetAllUserHandler(AppDbContext dbContext, Users user)
{

    public async Task<Users> GetAllUserAsync(CancellationToken ct)
    {


        return user;
    }

}


public sealed class GetAllUser : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/v1/user", async)
    }
}