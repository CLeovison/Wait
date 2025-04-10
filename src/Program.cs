using System.Reflection;
using Microsoft.EntityFrameworkCore;

using Wait.Contracts.Data;
using Wait.Database;
using Wait.Entities;
using Wait.Extensions;
using Wait.Repositories;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());


var app = builder.Build();
app.Endpoint();

app.MapPost("/api/create", async (UserRepositories repositories,
            IDbContextFactory<AppDbContext> dbContext,
            UserDto dto,
            CancellationToken cancellationToken) =>
{
    var connection = await dbContext.CreateDbContextAsync(cancellationToken);
    Users users = new Users
    {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Username = dto.Username,
        Password = dto.Password,
        Email = dto.Email,
    };

    await connection.User.AddAsync(users, cancellationToken);
    await connection.SaveChangesAsync();

});

app.Run();


