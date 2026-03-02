using LibraryLending.Application.Core.Abstractions.Validations;
using LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;
using LibraryLending.Domain.Aggregates.Members.MemberNumbers;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Features.Lending.BorrowBookCopy;


public sealed class BorrowBookCopyValidator : IValidator<BorrowBookCopyCommand>
{
    public Error? Validate(BorrowBookCopyCommand request)
    {
        if (string.IsNullOrWhiteSpace(request.MemberNumber))
            return MemberNumberErrors.Empty;

        if (string.IsNullOrWhiteSpace(request.CopyBarcode))
            return CopyBarcodeErrors.Empty;

        return null;
    }
}
