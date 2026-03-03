using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using LibraryLending.Infrastructure.Persistence.EfCore.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryLending.Infrastructure.DependencyInjection;


public static class InfrastructureInitializationExtensions
{
    public static async Task InitializeInfrastructureAsync(
        this IServiceProvider services,
        CancellationToken ct = default)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

        await DatabaseInitializer.InitializeAsync(db, ct);
    }
}
