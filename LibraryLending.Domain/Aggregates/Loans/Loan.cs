using LibraryLending.Domain.Aggregates.Books.BookCopies;
using LibraryLending.Domain.Aggregates.Members;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Loans;

public sealed class Loan
{
    public LoanId Id { get; }
    public MemberId MemberId { get; }
    public BookCopyId BookCopyId { get; }
    public DateTime LoanDate { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnedAt { get; private set; }
    public bool IsActive => ReturnedAt is null;

    private Loan(LoanId id, MemberId memberId, BookCopyId bookCopyId, DateTime loanDate, DateTime dueDate, DateTime? returnedAt)
        => (Id, MemberId, BookCopyId, LoanDate, DueDate, ReturnedAt) = (id, memberId, bookCopyId, loanDate, dueDate, returnedAt);

    internal static Result<Loan> Create(MemberId memberId, BookCopyId bookCopyId, DateTime loanDate, DateTime dueDate)
    {
        if (dueDate <= loanDate)
            return LoanErrors.InvalidDates;

        return new Loan(
            id: LoanId.New(),
            memberId: memberId,
            bookCopyId: bookCopyId,
            loanDate: loanDate,
            dueDate: dueDate,
            returnedAt: null
        );
    }

    internal static Loan Rehydrate(LoanId id, MemberId memberId, BookCopyId bookCopyId, DateTime loanDate, DateTime dueDate, DateTime? returnedAt)
        => new(id, memberId, bookCopyId, loanDate, dueDate, returnedAt);

    public Result Return(DateTime returnedAt)
    {
        if (ReturnedAt is not null)
            return LoanErrors.AlreadyReturned;

        ReturnedAt = returnedAt;
        return Result.Success();
    }
}
