using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Seeding;

internal static class DatabaseInitializer
{
    public static async Task InitializeAsync(LibraryDbContext db, CancellationToken ct = default)
    {
        await db.Database.MigrateAsync(ct);

        await LendingPolicySeeder.SeedAsync(db, ct);
        await TestDataSeeder.SeedAsync(db, ct);
    }
}
