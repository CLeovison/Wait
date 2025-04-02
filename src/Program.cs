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

app.MapGet("/api/{id}", async (Guid id, AppDbContext dbContext) =>
{

    return await dbContext.User.FindAsync(id);
});

app.MapPut("/api/update/{id}", async (Guid id, AppDbContext dbContext, User user) =>
{
    dbContext.User.Update(user);
    return await dbContext.SaveChangesAsync();
});


app.MapDelete("/api/{id}", async (Guid id, AppDbContext dbContext) =>
{   
    var result = await dbContext.User.Where(x => x.UserId == id).ExecuteDeleteAsync();
    return result > 0;
});
app.Run();


