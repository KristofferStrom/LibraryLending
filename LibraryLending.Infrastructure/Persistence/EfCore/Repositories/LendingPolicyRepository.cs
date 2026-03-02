using LibraryLending.Domain.Aggregates.LendingPolicies;
using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Repositories;

internal sealed class LendingPolicyRepository(LibraryDbContext db) : ILendingPolicyRepository
{
    public async Task<LendingPolicy?> GetCurrentAsync(CancellationToken ct)
    {

        LendingPolicyEntity? entity = await db.LendingPolicies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.IsActive, ct);

        if (entity is null) return null;

        return LendingPolicy.Rehydrate(
            id: new LendingPolicyId(entity.Id),
            loanDays: entity.LoanDays,
            isActive: entity.IsActive
        );
    }
}