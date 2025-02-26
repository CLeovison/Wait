using Microsoft.EntityFrameworkCore;
using Wait.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration["ConnectionStrings: DefaultConnection"]!);
});

var app = builder.Build();


app.Run();
