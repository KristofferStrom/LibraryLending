using FluentAssertions;
using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.SharedKernel.Results;
using LibraryLending.Testing.TestData;

namespace LibraryLending.Domain.Tests.Members;

public class MemberBorrowTests
{
    [Fact]
    public void Borrow_WhenMemberIsInactive_ShouldReturnMemberInactiveError()
    {
        var member = DomainFactory.InactiveMember();
        var copy = DomainFactory.AvailableCopy();
        var loanDate = DateTime.UtcNow;

        Result<Loan> result = member.Borrow(copy, loanDate, 14, Array.Empty<BookId>());

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MemberErrors.Inactive);
        result.Error.Type.Should().Be(ErrorType.Conflict);
    }

    [Fact]
    public void Borrow_WhenMemberAlreadyHasSameBookActive_ShouldReturnAlreadyBorrowingSameBookError()
    {
        var member = DomainFactory.ActiveMember();
        var copy = DomainFactory.AvailableCopy(bookId: DomainFactory.NewBookId());

        var activeLoanBookIds = new[] { copy.BookId };

        Result<Loan> result = member.Borrow(copy, DateTime.UtcNow, 14, activeLoanBookIds);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MemberErrors.AlreadyBorrowingSameBook);
        result.Error.Type.Should().Be(ErrorType.Conflict);
    }

    [Fact]
    public void Borrow_WhenCopyCannotBeMarkedOnLoan_ShouldReturnErrorFromCopy()
    {
        var member = DomainFactory.ActiveMember();
        var copy = DomainFactory.OnLoanCopy();

        Result<Loan> result = member.Borrow(copy, DateTime.UtcNow, 14, Array.Empty<BookId>());

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BookCopyErrors.NotAvailable);
    }

    [Fact]
    public void Borrow_WhenRulesPass_ShouldCreateLoan_AndMarkCopyOnLoan()
    {
        var member = DomainFactory.ActiveMember();
        var copy = DomainFactory.AvailableCopy();
        var loanDate = DateTime.UtcNow;

        Result<Loan> result = member.Borrow(copy, loanDate, 14, Array.Empty<BookId>());

        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);

        var loan = result.Value;
        loan.MemberId.Should().Be(member.Id);
        loan.BookCopyId.Should().Be(copy.Id);
        loan.LoanDate.Should().Be(loanDate);
        loan.DueDate.Should().Be(loanDate.AddDays(14));
        loan.IsActive.Should().BeTrue();

        copy.Status.Should().Be(CopyStatus.OnLoan);
    }
}