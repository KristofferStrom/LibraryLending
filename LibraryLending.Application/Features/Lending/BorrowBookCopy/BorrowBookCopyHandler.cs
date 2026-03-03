using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;
using LibraryLending.Domain.Aggregates.LendingPolicies;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.Domain.Aggregates.Members.MemberNumbers;
using LibraryLending.Domain.Shared.Abstractions;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Features.Lending.BorrowBookCopy;

public sealed class BorrowBookCopyHandler(
    IMemberRepository members,
    IBookCopyRepository bookCopies,
    ILendingPolicyRepository lendingPolicies,
    ILoanRepository loans,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<BorrowBookCopyCommand, Result<LoanId>>
{
    public async Task<Result<LoanId>> Handle(BorrowBookCopyCommand command, CancellationToken ct)
    {
        var memberNumberResult = MemberNumber.Create(command.MemberNumber);
        if (memberNumberResult.IsFailure) return memberNumberResult.Error;

        var barcodeResult = CopyBarcode.Create(command.CopyBarcode);
        if (barcodeResult.IsFailure) return barcodeResult.Error;

        Member? member = await members.GetByMemberNumberAsync(memberNumberResult.Value, ct);
        if (member is null) return BorrowBookCopyErrors.MemberNotFound;

        BookCopy? copy = await bookCopies.GetByBarcodeAsync(barcodeResult.Value, ct);
        if (copy is null) return BorrowBookCopyErrors.BookCopyNotFound;

        LendingPolicy? policy = await lendingPolicies.GetCurrentAsync(ct);
        if (policy is null) return BorrowBookCopyErrors.LendingPolicyMissing;

        IReadOnlyCollection<BookId> activeLoanBookIds = await loans.GetActiveLoanBookIdsForMemberAsync(member.Id, ct);

        var borrowResult = member.Borrow(copy, DateTime.UtcNow, policy.LoanDays, activeLoanBookIds);
        if (borrowResult.IsFailure) return borrowResult.Error;

        var loan = borrowResult.Value;

        loans.Add(loan);
        bookCopies.UpdateStatus(copy.Id, copy.Status);

        await unitOfWork.SaveChangesAsync(ct);

        return loan.Id;
    }
}
