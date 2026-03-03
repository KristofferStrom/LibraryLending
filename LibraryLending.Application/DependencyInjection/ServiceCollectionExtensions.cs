using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.Application.Core.Messaging;
using LibraryLending.Application.Core.Pipelines.Performance;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryLending.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        services.Scan(scan => scan
            .FromAssemblies(typeof(ServiceCollectionExtensions).Assembly)

            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        services.Decorate(typeof(ICommandHandler<,>), typeof(SlowCommandLoggingDecorator<,>));

        return services;
    }
}
