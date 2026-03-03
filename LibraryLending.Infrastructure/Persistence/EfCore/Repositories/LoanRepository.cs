using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.Infrastructure.Persistence.EfCore.Contexts;
using LibraryLending.Infrastructure.Persistence.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryLending.Infrastructure.Persistence.EfCore.Repositories;


internal sealed class LoanRepository(LibraryDbContext db) : ILoanRepository
{
    public void Add(Loan loan)
    {
        var entity = new LoanEntity
        {
            Id = loan.Id.Value,
            MemberId = loan.MemberId.Value,
            BookCopyId = loan.BookCopyId.Value,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
            ReturnedAt = loan.ReturnedAt
        };

        db.Loans.Add(entity);
    }

    public async Task<IReadOnlyCollection<BookId>> GetActiveLoanBookIdsForMemberAsync(MemberId memberId, CancellationToken ct)
    {
        var bookIds = await db.Loans
            .AsNoTracking()
            .Where(l => l.MemberId == memberId.Value && l.ReturnedAt == null)
            .Join(
                db.BookCopies.AsNoTracking(),
                loan => loan.BookCopyId,
                copy => copy.Id,
                (loan, copy) => copy.BookId
            )
            .Distinct()
            .ToListAsync(ct);

        return bookIds.Select(id => new BookId(id)).ToList();
    }

    public async Task<Loan?> GetActiveByBookCopyIdAsync(BookCopyId copyId, CancellationToken ct)
    {
        var entity = await db.Loans
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.BookCopyId == copyId.Value && x.ReturnedAt == null, ct);

        if (entity is null) return null;

        return Loan.Rehydrate(
            id: new LoanId(entity.Id),
            memberId: new MemberId(entity.MemberId),
            bookCopyId: new BookCopyId(entity.BookCopyId),
            loanDate: entity.LoanDate,
            dueDate: entity.DueDate,
            returnedAt: entity.ReturnedAt
        );
    }

    public void UpdateReturnedAt(LoanId loanId, DateTime returnedAt)
    {
        var entity = new LoanEntity { Id = loanId.Value };

        db.Loans.Attach(entity);

        entity.ReturnedAt = returnedAt;
        db.Entry(entity).Property(x => x.ReturnedAt).IsModified = true;
    }
}
