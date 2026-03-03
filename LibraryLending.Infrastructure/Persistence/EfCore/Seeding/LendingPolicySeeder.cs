using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Seeding;

internal static class LendingPolicySeeder
{
    public static async Task SeedAsync(LibraryDbContext db, CancellationToken ct = default)
    {
        var hasActive = await db.LendingPolicies.AnyAsync(x => x.IsActive, ct);
        if (hasActive) return;

        db.LendingPolicies.Add(new LendingPolicyEntity
        {
            Id = Guid.NewGuid(),
            LoanDays = 14,
            IsActive = true
        });

        await db.SaveChangesAsync(ct);
    }
}