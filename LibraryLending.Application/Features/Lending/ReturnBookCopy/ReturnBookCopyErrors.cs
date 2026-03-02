using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Features.Lending.ReturnBookCopy;

public class ReturnBookCopyErrors
{
    public static Error BookCopyNotFound => new(ErrorType.NotFound, "Exemplar hittades inte.");
    public static Error ActiveLoanNotFound => new(ErrorType.NotFound, "Aktivt lån för exemplar hittades inte.");
}