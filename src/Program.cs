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

app.MapPost("/api/create", async (CreateUserRequest request, AppDbContext dbContext, CancellationToken cancellationToken) =>
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
    var existingUser = dbContext.User.Find(request.UserId);


    if (existingUser is not null)
    {
        throw new ArgumentException("The User already exist");
    }
    else
    {
        return Results.Ok();
    }
});


app.MapGet("/api", async (AppDbContext dbContext) =>
{
    return await dbContext.User.ToListAsync();
});

app.MapGet("/api/{id}", async (int id, AppDbContext dbContext, User user) =>
{
    var users = dbContext.User.Find(user.UserId);

    if(users is null){
        throw new ArgumentException("The user doesn't exist");
    }

    return await dbContext.User.;
});

app.Run();


