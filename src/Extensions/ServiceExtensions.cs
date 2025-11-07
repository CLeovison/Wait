//Repository Collection
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

using Wait.Abstract;
using Wait.Domain.Entities;
using Wait.Infrastructure.Authentication;
using Wait.Infrastructure.Repositories;
using Wait.Infrastructure.Repositories.CategoriesRepository;
using Wait.Infrastructure.Repositories.ProductRepository;
using Wait.Infrastructure.Repositories.UserRepository;

//Service Collection
using Wait.Services.AuthService;
using Wait.Services.Categories;
using Wait.Services.FileServices;
using Wait.Services.ImageServices;
using Wait.Services.ProductServices;
using Wait.Services.UserServices;

namespace Wait.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositoriesCollection(this IServiceCollection services)
    {
        services.AddScoped<IUserRepositories, UserRepositories>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        return services;
    }

    public static IServiceCollection AddServicesCollection(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<IImageService, ImageService>();
        return services;
    }

    public static IServiceCollection AddAuthenticationCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))

            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies["accessToken"];
                    return Task.CompletedTask;
                }
            };

        });
        services.AddAuthorization();
        services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
        services.AddScoped<ITokenProvider, TokenProvider>();
        return services;
    }
}