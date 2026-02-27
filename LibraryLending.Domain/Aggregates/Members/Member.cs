using LibraryLending.Domain.Aggregates.Members.MemberNumbers;
using LibraryLending.Domain.Shared.ValueObjects.FullNames;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Members;

public sealed class Member
{
    public MemberId Id { get; private set; }
    public MemberNumber MemberNumber { get; private set; } = null!;
    public FullName FullName { get; private set; } = null!;
    public bool IsActive { get; private set; }

    private Member(MemberId id, MemberNumber memberNumber, FullName fullName, bool isActive) =>
        (Id, MemberNumber, FullName, IsActive) = (id, memberNumber, fullName, isActive);

    public static Result<Member> Create(string memberNumber, string firstName, string lastName)
    {
        var fullNameResult = FullName.Create(firstName, lastName);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;

        var memberNumberResult = MemberNumber.Create(memberNumber);
        if (memberNumberResult.IsFailure)
            return memberNumberResult.Error;

        return new Member(
            id: MemberId.New(),
            memberNumber: memberNumberResult.Value,
            fullName: fullNameResult.Value,
            isActive: true
        );
    }

    internal static Member Rehydrate(MemberId id, string memberNumber, string firstName, string lastName, bool isActive) =>
        new(
            id,
            MemberNumber.Rehydrate(memberNumber),
            FullName.Rehydrate(firstName, lastName),
            isActive
        );
}
