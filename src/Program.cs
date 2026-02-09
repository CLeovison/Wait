
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wait.Database;
using Wait.Extensions;
using FluentValidation;
using Wait.Features.Users.CreateUser;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthorization();


builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Services.AddAuthenticationCollection(configuration);

builder.Services.AddValidatorsFromAssemblyContaining<Program>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.Endpoint();

app.Run();