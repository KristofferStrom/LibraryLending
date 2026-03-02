using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Books.BookCopies.CopyBarcodes;
using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Repositories;


internal sealed class BookCopyRepository(LibraryDbContext db) : IBookCopyRepository
{
    public async Task<BookCopy?> GetByBarcodeAsync(CopyBarcode barcode, CancellationToken ct)
    {
        var entity = await db.BookCopies
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Barcode == barcode.Value, ct);

        if (entity is null) return null;

        return BookCopy.Rehydrate(
            id: new BookCopyId(entity.Id),
            bookId: new BookId(entity.BookId),
            barcode: entity.Barcode,
            status: (CopyStatus)entity.Status
        );
    }

    public void UpdateStatus(BookCopyId id, CopyStatus status)
    {
        var entity = new BookCopyEntity { Id = id.Value };

        db.BookCopies.Attach(entity);

        entity.Status = (int)status;
        db.Entry(entity).Property(x => x.Status).IsModified = true;
    }
}
