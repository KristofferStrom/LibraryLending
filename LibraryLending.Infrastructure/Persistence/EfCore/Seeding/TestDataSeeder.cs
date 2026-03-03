using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Seeding;

internal static class TestDataSeeder
{
    public static async Task SeedAsync(LibraryDbContext db, CancellationToken ct = default)
    {
        const string memberNumber = "M-1001";
        var memberExists = await db.Members.AnyAsync(x => x.MemberNumber == memberNumber, ct);
        if (!memberExists)
        {
            db.Members.Add(new MemberEntity
            {
                Id = Guid.NewGuid(),
                MemberNumber = memberNumber,
                FirstName = "Test",
                LastName = "User",
                IsActive = true
            });

            await db.SaveChangesAsync(ct);
        }

        const string isbn = "9781234567890";
        var book = await db.Books.SingleOrDefaultAsync(x => x.Isbn == isbn, ct);
        if (book is null)
        {
            book = new BookEntity
            {
                Id = Guid.NewGuid(),
                Title = "Test Book",
                Isbn = isbn
            };

            db.Books.Add(book);
            await db.SaveChangesAsync(ct);
        }

        const string barcode = "BC-0001";
        var copyExists = await db.BookCopies.AnyAsync(x => x.Barcode == barcode, ct);
        if (!copyExists)
        {
            db.BookCopies.Add(new BookCopyEntity
            {
                Id = Guid.NewGuid(),
                BookId = book.Id,
                Barcode = barcode,
                Status = 0
            });

            await db.SaveChangesAsync(ct);
        }
    }
}