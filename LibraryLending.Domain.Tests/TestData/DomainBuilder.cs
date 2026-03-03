using FluentAssertions;
using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Members;

namespace LibraryLending.Domain.Tests.TestData;

internal static class DomainBuilder
{
    public static Member ActiveMember(string memberNumber = "M-0001")
    {
        var created = Member.Create(memberNumber, "Ada", "Lovelace");
        created.IsSuccess.Should().BeTrue("testdata ska vara giltig");
        return created.Value;
    }

    public static Member InactiveMember()
        => Member.Rehydrate(
            id: MemberId.New(),
            memberNumber: "M-9999",
            firstName: "Inactive",
            lastName: "Member",
            isActive: false);

    public static BookId NewBookId() => BookId.New();

    public static BookCopy AvailableCopy(BookId? bookId = null, string barcode = "BC-0001")
    {
        bookId ??= NewBookId();

        var copyResult = BookCopy.Create(bookId.Value, barcode);
        copyResult.IsSuccess.Should().BeTrue("testdata ska vara giltig");
        return copyResult.Value;
    }

    public static BookCopy OnLoanCopy()
    {
        var copy = AvailableCopy();
        copy.MarkOnLoan().IsSuccess.Should().BeTrue();
        return copy;
    }
}
