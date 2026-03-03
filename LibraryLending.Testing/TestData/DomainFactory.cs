using FluentAssertions;
using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.LendingPolicies;
using LibraryLending.Domain.Aggregates.Members;

namespace LibraryLending.Testing.TestData;


public static class DomainFactory
{
    public static Member ActiveMember(
        string memberNumber = "M-0001",
        string firstName = "Ada",
        string lastName = "Lovelace")
    {
        var created = Member.Create(memberNumber, firstName, lastName);
        created.IsSuccess.Should().BeTrue("testdata ska vara giltig");
        return created.Value;
    }

    public static Member InactiveMember(
        string memberNumber = "M-9999",
        string firstName = "Inactive",
        string lastName = "Member")
        => Member.Rehydrate(
            id: MemberId.New(),
            memberNumber: memberNumber,
            firstName: firstName,
            lastName: lastName,
            isActive: false);

    public static BookId NewBookId() => BookId.New();

    public static BookCopy AvailableCopy(
        BookId? bookId = null,
        string barcode = "BC-0001")
    {
        bookId ??= NewBookId();

        var created = BookCopy.Create(bookId.Value, barcode);
        created.IsSuccess.Should().BeTrue("testdata ska vara giltig");
        return created.Value;
    }

    public static BookCopy OnLoanCopy(
        BookId? bookId = null,
        string barcode = "BC-0002")
    {
        var copy = AvailableCopy(bookId, barcode);

        var mark = copy.MarkOnLoan();
        mark.IsSuccess.Should().BeTrue("setup: kopian ska kunna markeras OnLoan");

        return copy;
    }

    public static LendingPolicy ActivePolicy(int loanDays = 14)
        => LendingPolicy.Rehydrate(
            id: LendingPolicyId.New(),
            loanDays: loanDays,
            isActive: true);

    public static LendingPolicy InactivePolicy(int loanDays = 14)
        => LendingPolicy.Rehydrate(
            id: LendingPolicyId.New(),
            loanDays: loanDays,
            isActive: false);

    public static LendingPolicyId NewPolicyId()
        => LendingPolicyId.New();
}
