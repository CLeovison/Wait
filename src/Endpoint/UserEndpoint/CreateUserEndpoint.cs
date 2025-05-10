using Wait.Abstract;
using Wait.Contracts.Data;
using Wait.Contracts.Request.UserRequest;
using Wait.UserServices.Services;


namespace Wait.Endpoint.UserEndpoint;


public class CreateUserEndpoint(IUserServices userServices) : IEndpoint
{


    public void Endpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/create", async (CreateUserRequest req) =>
        {



            var userDto = new UserDto
            {
                UserId = req.UserId,
                FirstName = req.FirstName,
                LastName = req.LastName,
                Username = req.Username,
                Password = req.Password,
                Email = req.Email,


            };
            return await userServices.CreateUserAsync(userDto);
        });
    }
}