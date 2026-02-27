using LibraryLending.Domain.Aggregates.Members.ValueObjects.MemberNumbers;
using LibraryLending.Domain.Shared.ValueObjects.FullNames;

namespace LibraryLending.Domain.Aggregates.Members;

public sealed class Member
{
    public Guid Id { get; private set; }
    public MemberNumber MemberNumber { get; private set; } = null!;
    public FullName FullName { get; private set; } = null!;
    public bool IsActive { get; private set; }

    private Member(Guid id, MemberNumber memberNumber, FullName fullName, bool isActive) =>
        (Id, MemberNumber, FullName, IsActive) = (id, memberNumber, fullName, isActive);

    public static Member Create(string memberNumber, string firstName, string lastName)
    {
        var fullName = FullName.Create(firstName, lastName);
        var memberNum = MemberNumber.Create(memberNumber);

        return new(
            id: Guid.NewGuid(),
            memberNumber: memberNum,
            fullName: fullName,
            isActive: true
        );
    }

    internal static Member Rehydrate(Guid id, string memberNumber, string firstName, string lastName, bool isActive) =>
        new(
            id,
            MemberNumber.Rehydrate(memberNumber),
            FullName.Rehydrate(firstName, lastName),
            isActive
        );
}
