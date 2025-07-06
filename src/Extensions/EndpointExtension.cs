using System.Reflection;
using Wait.Abstract;

namespace Wait.Extensions;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.DefinedTypes
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

        foreach (var type in types)
        {
            services.AddScoped(typeof(IEndpoint), type); // Scoped because they depend on AppDbContext
        }

        return services;
    }

    public static WebApplication Endpoint(this WebApplication app)
    {
        var routeBuilder = (IEndpointRouteBuilder)app;

        // Temporary scope JUST for startup-time mapping
        using var scope = app.Services.CreateScope();
        var endpoints = scope.ServiceProvider.GetServices<IEndpoint>();

        foreach (var endpoint in endpoints)
        {
            endpoint.Endpoint(routeBuilder); // Only maps routes; no lifetime abuse
        }

        return app;
    }

    public static RouteHandlerBuilder HasPermission(this RouteHandlerBuilder app, string permission)
    {
        return app.RequireAuthorization(permission);
    }
}