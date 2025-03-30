using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wait.Contracts.Request.UserRequest;
using Wait.Database;
using Wait.Entities;
using Wait.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());


var app = builder.Build();
app.Endpoint();

app.MapPost("/create", async (CreateUserRequest request, AppDbContext dbContext, CancellationToken cancellationToken) =>
{
    User user = new()
    {
        UserId = Guid.NewGuid(),
        Username = request.Username,
        Password = request.Password,
        Email = request.Email,
        FirstName = request.FirstName,
        LastName = request.LastName
    };

    dbContext.User.Add(user);
    await dbContext.SaveChangesAsync(cancellationToken);

    return Results.Ok();
});
app.MapGet("/", async (AppDbContext dbContext) =>
{   var user = await dbContext.User.ToListAsync();
    return Results.Ok();

});
app.Run();


