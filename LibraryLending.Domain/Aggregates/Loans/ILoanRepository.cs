using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Members;

namespace LibraryLending.Domain.Aggregates.Loans;

public interface ILoanRepository
{
    void Add(Loan loan);
    Task<Loan?> GetActiveByBookCopyIdAsync(BookCopyId id, CancellationToken ct);
    Task<IReadOnlyCollection<BookId>> GetActiveLoanBookIdsForMemberAsync(MemberId id, CancellationToken ct);
    void UpdateReturnedAt(LoanId id, DateTime value);
}
