using LibraryLending.Api.Abstractions;
using System.Reflection;

namespace LibraryLending.Api.Extensions;


public static class EndpointRegistrationExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var endpoints = assembly
            .DefinedTypes
            .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IEndpoint).IsAssignableFrom(t))
            .Select(t => t.AsType());

        foreach (var endpoint in endpoints)
            services.AddSingleton(typeof(IEndpoint), endpoint);

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (var endpoint in endpoints)
            endpoint.MapEndpoint(app);

        return app;
    }
}
