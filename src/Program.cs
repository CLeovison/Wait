// Microsoft Packages
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Data Access
using Wait.Database;
using Wait.Domain.Entities;

// Business Logic Access
using Wait.Extensions;
using Wait.Infrastructure.Authentication;

// Validation
using FluentValidation;
using Wait.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Configure EF Core with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Configure authentication/authorization
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddRateLimiter();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Services.AddRepositoriesCollection();
builder.Services.AddServicesCollection();

builder.Services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

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