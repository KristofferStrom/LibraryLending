using FluentAssertions;
using LibraryLending.Application.Features.Lending.BorrowBookCopy;
using LibraryLending.Application.Tests.TestData;
using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.Domain.Aggregates.Members.MemberNumbers;
using LibraryLending.SharedKernel.Results;
using NSubstitute;

namespace LibraryLending.Application.Tests.Lending;


public class BorrowBookCopyHandlerTests
{
    [Fact]
    public async Task Handle_HappyPath_AddsLoan_UpdatesCopyStatus_AndSaves()
    {
        // Arrange
        var fx = new ApplicationTestFixture();
        var handler = new BorrowBookCopyHandler(
            fx.Members, fx.BookCopies, fx.LendingPolicies, fx.Loans, fx.UnitOfWork);

        var command = new BorrowBookCopyCommand("M-0001", "BC-0001");

        var memberNumber = MemberNumber.Create(command.MemberNumber).Value;
        var barcode = CopyBarcode.Create(command.CopyBarcode).Value;

        var member = TestDataFactory.ActiveMember("M-0001");
        var copy = TestDataFactory.AvailableCopy("BC-0001");
        var policy = TestDataFactory.ActivePolicy(loanDays: 14);

        fx.Members.GetByMemberNumberAsync(memberNumber, Arg.Any<CancellationToken>()).Returns(member);
        fx.BookCopies.GetByBarcodeAsync(barcode, Arg.Any<CancellationToken>()).Returns(copy);
        fx.LendingPolicies.GetCurrentAsync(Arg.Any<CancellationToken>()).Returns(policy);
        fx.Loans.GetActiveLoanBookIdsForMemberAsync(member.Id, Arg.Any<CancellationToken>())
                .Returns(Array.Empty<BookId>());

        // Act
        Result<LoanId> result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        fx.Loans.Received(1).Add(Arg.Any<Loan>());
        fx.BookCopies.Received(1).UpdateStatus(copy.Id, CopyStatus.OnLoan);
        await fx.UnitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WhenMemberNotFound_ReturnsMemberNotFound_AndDoesNotSave()
    {
        // Arrange
        var fx = new ApplicationTestFixture();
        var handler = new BorrowBookCopyHandler(
            fx.Members, fx.BookCopies, fx.LendingPolicies, fx.Loans, fx.UnitOfWork);

        var command = new BorrowBookCopyCommand("M-404", "BC-0001");
        var memberNumber = MemberNumber.Create(command.MemberNumber).Value;

        fx.Members.GetByMemberNumberAsync(memberNumber, Arg.Any<CancellationToken>())
                  .Returns((Member?)null);

        // Act
        Result<LoanId> result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(BorrowBookCopyErrors.MemberNotFound);

        await fx.UnitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
        fx.Loans.DidNotReceiveWithAnyArgs().Add(default!);
        fx.BookCopies.DidNotReceiveWithAnyArgs().UpdateStatus(default!, default!);
    }
}
