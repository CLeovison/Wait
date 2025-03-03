using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wait.Database;
using Wait.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration["ConnectionStrings: DefaultConnection"]!);
});
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());


var app = builder.Build();
app.Endpoint();


app.Run();
