using LibraryLending.Domain.Aggregates.Books;
using LibraryLending.Domain.Aggregates.Members;

namespace LibraryLending.Domain.Aggregates.Loans;

public interface ILoanRepository
{
    void Add(Loan loan);
    Task<IReadOnlyCollection<BookId>> GetActiveLoanBookIdsForMemberAsync(MemberId id, CancellationToken ct);
}
