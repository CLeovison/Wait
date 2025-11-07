using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Contracts.Data;
using Wait.Services.UserServices;
using Wait.Domain.Entities;
using Wait.Extensions;
using Wait.Contracts.Request.UserRequest;
using Wait.Infrastructure.Mapping;


namespace Wait.Endpoint.UserEndpoint;

public sealed class UpdateUserEndpoint : IEndpoint
{
    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/users/{id}", async (Guid id, UpdateUserRequest request, IUserServices userServices, CancellationToken ct) =>
        {
            var updatedUser = request.ToRequestUpdate();
            var result = await userServices.UpdateUserAsync(id, updatedUser, ct);

            return result is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(result);
        });


    }
}