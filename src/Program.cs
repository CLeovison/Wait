//Microsoft Packages
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

//Data Access
using Wait.Database;
using Wait.Domain.Entities;
using Wait.Infrastracture.Repositories;

//Business Logic Access
using Wait.Services.UserServices;
using Wait.Extensions;
using Wait.Infrastracture;

//Validation
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Register the DbContextFactory
builder.Services.AddDbContextFactory<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddRateLimiter();

// Register other services
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IUserRepositories, UserRepositories>();
builder.Services.AddSingleton<IUserServices, UserServices>();
builder.Services.AddSingleton<IPasswordHasher<Users>, PasswordHasher<Users>>();
builder.Services.AddSingleton<TokenProvider>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);



var app = builder.Build();
app.Endpoint();
app.UseAuthorization();
app.UseAuthentication();
app.UseRateLimiter();
app.Run();