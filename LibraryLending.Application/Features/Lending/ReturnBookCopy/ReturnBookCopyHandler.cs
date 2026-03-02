using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.Domain.Shared.Abstractions;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Features.Lending.ReturnBookCopy;

public sealed class ReturnBookCopyHandler(
    IBookCopyRepository bookCopies,
    ILoanRepository loans,
    IUnitOfWork unitOfWork
)
    : ICommandHandler<ReturnBookCopyCommand, Result>
{
    public async Task<Result> Handle(ReturnBookCopyCommand command, CancellationToken ct)
    {
        var barcodeResult = CopyBarcode.Create(command.CopyBarcode);
        if (barcodeResult.IsFailure) return barcodeResult.Error;

        BookCopy? bookCopy = await bookCopies.GetByBarcodeAsync(barcodeResult.Value, ct);
        if (bookCopy is null) return ReturnBookCopyErrors.BookCopyNotFound;

        Loan? loan = await loans.GetActiveByBookCopyIdAsync(bookCopy.Id, ct);
        if (loan is null) return ReturnBookCopyErrors.ActiveLoanNotFound;

        var returnResult = loan.Return(DateTime.UtcNow);
        if (returnResult.IsFailure) return returnResult.Error;

        var markAvailableResult = bookCopy.MarkAvailable();
        if (markAvailableResult.IsFailure) return markAvailableResult.Error;

        loans.UpdateReturnedAt(loan.Id, loan.ReturnedAt!.Value);
        bookCopies.UpdateStatus(bookCopy.Id, bookCopy.Status);

        await unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}
