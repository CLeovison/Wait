// Microsoft Packages
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// Data Access
using Wait.Database;
using Wait.Domain.Entities;
using Wait.Infrastructure.Repositories.UserRepository;

// Business Logic Access
using Wait.Services.UserServices;
using Wait.Extensions;
using Wait.Infrastructure.Authentication;

// Validation
using FluentValidation;
using Wait.Infrastructure.Repositories.ProductRepository;
using Wait.Services.ProductServices;
using Wait.Abstract;
using Wait.Services.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Configure EF Core with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

// Configure authentication/authorization
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddRateLimiter();

// Register minimal API endpoint definitions from assembly
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

// App services
builder.Services.AddScoped<IUserRepositories, UserRepositories>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Stateless services
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