using LibraryLending.Application.Core.Abstractions.Messaging;
using LibraryLending.Domain.Aggregates.Loans;
using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Features.Lending.BorrowBookCopy;

public sealed record BorrowBookCopyCommand(
    string MemberNumber,
    string CopyBarcode
) : ICommand<Result<LoanId>>, IHasSlowLogThreshold
{
    public int SlowLogThresholdMs => 200;
}