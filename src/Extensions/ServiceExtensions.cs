//Repository Collection
using Microsoft.AspNetCore.Identity;
using Wait.Abstract;
using Wait.Domain.Entities;
using Wait.Infrastructure.Authentication;
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

    public static IServiceCollection AddAuthenticationCollection(this IServiceCollection services)
    {
        services.AddAuthentication()
        .AddJwtBearer();

        services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        return services;
    }
}