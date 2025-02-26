using System.Reflection;
using Wait.Abstract;

namespace Wait.Extensions;


public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoint(this IServiceCollection services, Assembly assembly)
    {
        ServiceDescriptor[] serviceDescriptors = assembly.DefinedTypes
        .Where(type => type is { IsAbstract: false, IsInterface: false } && type.IsAssignableFrom(typeof(IEndpoint)))
        .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
        .ToArray();

        return services;
    }

    public static IApplicationBuilder Endpoint(this WebApplication app)
    {

    }
}