using LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;

namespace LibraryLending.Domain.Aggregates.Books.BookCopies;

public interface IBookCopyRepository
{
    Task<BookCopy?> GetByBarcodeAsync(CopyBarcode value, CancellationToken ct);
    void UpdateStatus(BookCopyId id, CopyStatus status);
}
