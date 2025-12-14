using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();
builder.Services.AddRateLimiter();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.Run();