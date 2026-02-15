
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wait.Database;
using Wait.Extensions;
using FluentValidation;
using Wait.Features.Users.CreateUser;
using Wait.Features.Users.GetAlluser;
using Wait.Features.Users.GetUserById;
using Wait.Features.Users.DeleteUser;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<GetAllUserHandler>();
builder.Services.AddScoped<GetUserByIdHandler>();
builder.Services.AddScoped<DeleteUserHandler>();
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