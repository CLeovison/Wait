using System.Reflection;

using Microsoft.EntityFrameworkCore;

using Wait.Database;

using Wait.Extensions;

using Wait.Repositories;
using Wait.Services.UserServices;
using Wait.UserServices.Services;


var builder = WebApplication.CreateBuilder(args);

// Register the DbContextFactory
builder.Services.AddDbContextFactory<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});

// Register other services
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IUserRepositories, UserRepositories>();
builder.Services.AddSingleton<IUserServices, UserServices>();

var app = builder.Build();
app.Endpoint();

app.Run();