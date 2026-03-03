namespace LibraryLending.Domain.Aggregates.Books.BookCopies;

using LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;
using LibraryLending.SharedKernel.Results;

public sealed class BookCopy
{
    public BookCopyId Id { get; }
    public BookId BookId { get; private set; }
    public CopyBarcode Barcode { get; private set; }
    public CopyStatus Status { get; private set; }

    private BookCopy(BookCopyId id, BookId bookId, CopyBarcode barcode, CopyStatus status) =>
        (Id, BookId, Barcode, Status) = (id, bookId, barcode, status);

    public static Result<BookCopy> Create(BookId bookId, string barcode)
    {
        var barcodeResult = CopyBarcode.Create(barcode);
        if (barcodeResult.IsFailure)
            return barcodeResult.Error;

        return new BookCopy(BookCopyId.New(), bookId, barcodeResult.Value, CopyStatus.Available);
    }

    internal static BookCopy Rehydrate(BookCopyId id, BookId bookId, string barcode, CopyStatus status) =>
    new(id, bookId, CopyBarcode.Rehydrate(barcode), status);

    internal Result MarkOnLoan()
    {
        if (Status != CopyStatus.Available)
            return BookCopyErrors.NotAvailable;

        Status = CopyStatus.OnLoan;
        return Result.Success();
    }

    public Result MarkAvailable()
    {
        if (Status != CopyStatus.OnLoan)
            return BookCopyErrors.NotOnLoan;

        Status = CopyStatus.Available;
        return Result.Success();
    }
}