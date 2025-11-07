using Wait.Abstract;
using Wait.Contracts.Request.UserRequest;
using Wait.Extensions;
using Wait.Infrastructure.Mapping;
using Wait.Services.UserServices;

namespace Wait.Endpoint.UserEndpoint;

public class CreateUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/create", async (CreateUserRequest req, IUserServices userServices, CancellationToken ct) =>
        {
            try
            {
                var userDto = req.ToCreateRequest();
                var userCreated = await userServices.CreateUserAsync(userDto, ct);
                var response = userCreated.ToUserResponse();
                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, title: "An error occurred while creating the user.");

            }
        })
        .WithValidation<CreateUserRequest>();
    }
}
