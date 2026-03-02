using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.Application.Core.Abstractions.Validations;
using LibraryLending.Application.Core.Messaging;
using LibraryLending.Application.Core.Pipelines.Performance.Application.Core.Pipelines.Performance;
using LibraryLending.Application.Core.Pipelines.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryLending.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IDispatcher, Dispatcher>();

        services.Scan(scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)

            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(c => c.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );

        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationCommandDecorator<,>));
        services.Decorate(typeof(IQueryHandler<,>), typeof(ValidationQueryDecorator<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(SlowCommandLoggingDecorator<,>));

        return services;
    }
}
