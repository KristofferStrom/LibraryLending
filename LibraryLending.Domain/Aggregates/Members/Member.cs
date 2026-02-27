using LibraryLending.Domain.Shared.ValueObjects.FullNames;

namespace LibraryLending.Domain.Aggregates.Members;

public sealed class Member
{
    public Guid Id { get; private set; }
    public MemberNumber MemberNumber { get; private set; } = null!;
    public FullName FullName { get; private set; } = null!;
    public bool IsActive { get; private set; }

    private Member(Guid id, string memberNumber, FullName fullName, bool isActive) =>
        (Id, MemberNumber, FullName, IsActive) = (id, memberNumber, fullName, isActive);

    public static Member Create(string memberNumber, string firstName, string lastName)
    {


        var fullName = FullName.Create(firstName, lastName);

        return new(
            id: Guid.NewGuid(),
            memberNumber: memberNumber,
            fullName: fullName,
            isActive: true
        );
    }

    internal static Member Rehydrate(Guid id, string memberNumber, string firstName, string lastName, bool isActive) =>
        new(id, memberNumber, FullName.Rehydrate(firstName, lastName), isActive);
}
