using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Contracts.Request.UserRequest;
using Wait.Entities;
using Wait.Services.UserServices;
using Wait.Mapping;
using Microsoft.AspNetCore.Mvc;
using Wait.UserServices.Services;


namespace Wait.Endpoint.UserEndpoint;


public class CreateUserEndpoint(IUserServices userServices) : IEndpoint
{


    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/create", async (CreateUserRequest req) =>
        {


            await userServices.CreateUserAsync(users);
        });
    }
}