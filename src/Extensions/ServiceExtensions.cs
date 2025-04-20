using Microsoft.EntityFrameworkCore;
using Wait.Database;


namespace Wait.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connection)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));

        return services;
    }
}