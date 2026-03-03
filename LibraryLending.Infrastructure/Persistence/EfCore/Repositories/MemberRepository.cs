using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.Domain.Aggregates.Members.MemberNumbers;
using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Repositories;

internal sealed class MemberRepository(LibraryDbContext db) : IMemberRepository
{
    public async Task<Member?> GetByMemberNumberAsync(MemberNumber memberNumber, CancellationToken ct)
    {
        MemberEntity? entity = await db.Members
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.MemberNumber == memberNumber.Value, ct);

        if (entity is null) return null;

        return Member.Rehydrate(
            id: new MemberId(entity.Id),
            memberNumber: entity.MemberNumber,
            firstName: entity.FirstName,
            lastName: entity.LastName,
            isActive: entity.IsActive
        );
    }
}
