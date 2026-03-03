using FluentAssertions;
using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.Domain.Tests.TestData;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Tests.Members;


public class MemberBorrowTests
{
    [Fact]
    public void Borrow_WhenMemberIsInactive_ShouldReturnMemberInactiveError()
    {
        // Arrange
        var member = DomainBuilder.InactiveMember();
        var copy = DomainBuilder.AvailableCopy();
        var loanDate = DateTime.UtcNow;

        // Act
        Result<Loan> result = member.Borrow(
            copy,
            loanDate,
            loanDays: 14,
            activeLoanBookIds: Array.Empty<BookId>());

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MemberErrors.Inactive);
        result.Error.Type.Should().Be(ErrorType.Conflict);
    }

    [Fact]
    public void Borrow_WhenMemberAlreadyHasSameBookActive_ShouldReturnAlreadyBorrowingSameBookError()
    {
        // Arrange
        var member = DomainBuilder.ActiveMember();
        var copy = DomainBuilder.AvailableCopy(bookId: DomainBuilder.NewBookId());

        var activeLoanBookIds = new[] { copy.BookId };

        // Act
        Result<Loan> result = member.Borrow(
            copy,
            loanDate: DateTime.UtcNow,
            loanDays: 14,
            activeLoanBookIds: activeLoanBookIds);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MemberErrors.AlreadyBorrowingSameBook);
        result.Error.Type.Should().Be(ErrorType.Conflict);
    }

    [Fact]
    public void Borrow_WhenCopyCannotBeMarkedOnLoan_ShouldReturnErrorFromCopy()
    {
        // Arrange
        var member = DomainBuilder.ActiveMember();
        var copy = DomainBuilder.OnLoanCopy();

        // Act
        Result<Loan> result = member.Borrow(
            copy,
            loanDate: DateTime.UtcNow,
            loanDays: 14,
            activeLoanBookIds: Array.Empty<BookId>());

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BookCopyErrors.NotAvailable);
    }

    [Fact]
    public void Borrow_WhenRulesPass_ShouldCreateLoan_AndMarkCopyOnLoan()
    {
        // Arrange
        var member = DomainBuilder.ActiveMember();
        var copy = DomainBuilder.AvailableCopy();
        var loanDate = DateTime.UtcNow;

        // Act
        Result<Loan> result = member.Borrow(
            copy,
            loanDate,
            loanDays: 14,
            activeLoanBookIds: Array.Empty<BookId>());

        // Assert
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
