using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Features.Lending.BorrowBookCopy;

public class BorrowBookCopyErrors
{
    public static Error MemberNotFound => new(ErrorType.NotFound, "Medlem hittades inte.");
    public static Error BookCopyNotFound => new(ErrorType.NotFound, "Exemplar hittades inte.");
    public static Error LendingPolicyMissing => new(ErrorType.NotFound, "Utlåningspolicy saknas.");

}
