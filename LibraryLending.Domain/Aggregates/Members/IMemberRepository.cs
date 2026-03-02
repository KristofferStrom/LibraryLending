using LibraryLending.Domain.Aggregates.Members.MemberNumbers;

namespace LibraryLending.Domain.Aggregates.Members;

public interface IMemberRepository
{
    Task<Member?> GetByMemberNumberAsync(MemberNumber value, CancellationToken ct);
}
