using FluentAssertions;
using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.LendingPolicies;
using LibraryLending.Domain.Aggregates.Members;

namespace LibraryLending.Application.Tests.TestData;

internal static class TestDataFactory
{
    public static Member ActiveMember(string memberNumber = "M-0001")
    {
        var created = Member.Create(memberNumber, "Ada", "Lovelace");
        created.IsSuccess.Should().BeTrue();
        return created.Value;
    }

    public static BookCopy AvailableCopy(string barcode = "BC-0001")
    {
        var created = BookCopy.Create(BookId.New(), barcode);
        created.IsSuccess.Should().BeTrue();
        return created.Value;
    }

    public static LendingPolicy ActivePolicy(int loanDays = 14)
    {
        return LendingPolicy.Rehydrate(
            id: LendingPolicyId.New(),
            loanDays: loanDays,
            isActive: true);
    }
}
