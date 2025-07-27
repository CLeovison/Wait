//Repository Collection
using Wait.Infrastructure.Repositories;
using Wait.Infrastructure.Repositories.ProductRepository;
using Wait.Infrastructure.Repositories.UserRepository;

//Service Collection
using Wait.Services.AuthService;
using Wait.Services.ProductServices;
using Wait.Services.UserServices;

namespace Wait.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositoriesCollection(this IServiceCollection services)
    {
        services.AddScoped<IUserRepositories, UserRepositories>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAuthRepository, AuthRepostiory>();
        return services;
    }

    public static IServiceCollection AddServicesCollection(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}