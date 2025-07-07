using Wait.Abstract;
using Wait.Contracts.Request.UserRequest;
using Wait.Extensions;
using Wait.Infrastracture.Mapping;
using Wait.Services.UserServices;
using Wait.Validation;

namespace Wait.Endpoint.UserEndpoint;

public class CreateUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/create", async (
            CreateUserRequest req,
            IUserServices userServices,
            CancellationToken ct) =>
        {
            var userDto = req.ToRequest();
            var userCreated = await userServices.CreateUserAsync(userDto, ct);
            return Results.Ok(userCreated);
        })
        .WithValidation<CreateUserRequest>();

    }
}
