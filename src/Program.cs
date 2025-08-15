
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wait.Database;
using Wait.Extensions;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Configure EF Core with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddHttpContextAccessor();
// Configure authentication/authorization

builder.Services.AddAuthorization();
builder.Services.AddRateLimiter();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Services.AddRepositoriesCollection();
builder.Services.AddServicesCollection();
builder.Services.AddAuthenticationCollection(configuration);

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Build the app
var app = builder.Build();

// Middleware
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

// Register endpoints cleanly
app.Endpoint();

app.Run();