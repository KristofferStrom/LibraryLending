using FluentAssertions;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Tests.Loans;


public class LoanTests
{
    [Fact]
    public void Create_WhenDueDateIsSameOrBeforeLoanDate_ShouldReturnInvalidDates()
    {
        // Arrange
        var memberId = MemberId.New();
        var bookCopyId = BookCopyId.New();

        var loanDate = DateTime.UtcNow;
        var dueDate = loanDate;

        // Act
        Result<Loan> result = Loan.Create(memberId, bookCopyId, loanDate, dueDate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(LoanErrors.InvalidDates);
    }

    [Fact]
    public void Return_WhenLoanIsActive_ShouldSetReturnedAt_AndReturnSuccess()
    {
        // Arrange
        var memberId = MemberId.New();
        var bookCopyId = BookCopyId.New();

        var loanDate = DateTime.UtcNow;
        var dueDate = loanDate.AddDays(14);

        var create = Loan.Create(memberId, bookCopyId, loanDate, dueDate);
        create.IsSuccess.Should().BeTrue("testdata ska vara giltig");

        var loan = create.Value;
        loan.IsActive.Should().BeTrue();
        loan.ReturnedAt.Should().BeNull();

        var returnedAt = loanDate.AddDays(1);

        // Act
        Result result = loan.Return(returnedAt);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);

        loan.ReturnedAt.Should().Be(returnedAt);
        loan.IsActive.Should().BeFalse();
    }

    [Fact]
    public void Return_WhenAlreadyReturned_ShouldReturnAlreadyReturned()
    {
        // Arrange
        var memberId = MemberId.New();
        var bookCopyId = BookCopyId.New();

        var loanDate = DateTime.UtcNow;
        var dueDate = loanDate.AddDays(14);

        var loan = Loan.Create(memberId, bookCopyId, loanDate, dueDate).Value;

        var firstReturnedAt = loanDate.AddDays(1);
        loan.Return(firstReturnedAt).IsSuccess.Should().BeTrue();

        // Act
        Result result = loan.Return(firstReturnedAt.AddMinutes(5));

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(LoanErrors.AlreadyReturned);
    }
}