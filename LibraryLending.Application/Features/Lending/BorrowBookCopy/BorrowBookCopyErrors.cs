using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Application.Features.Lending.BorrowBookCopy;

public class BorrowBookCopyErrors
{
    public static Error MemberNotFound => new(ErrorType.NotFound, "Medlem hittades inte.");
    public static Error BookCopyNotFound => new(ErrorType.NotFound, "Exemplar hittades inte.");
    public static Error LendingPolicyMissing => new(ErrorType.NotFound, "Utlåningspolicy saknas.");

}

//public static Error MemberNumberIsRequired => new(ErrorType.Validation, "Medlemsnummer måste anges.");

//public static Error BarcodeIsRequired => new(ErrorType.Validation, "Streckkod måste anges.");
