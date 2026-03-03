using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Loans;

public static class LoanErrors
{
    public static Error InvalidDates =>
        new(ErrorType.Validation, "Förfallodatum måste vara efter lånedatum.");

    public static Error AlreadyReturned =>
        new(ErrorType.Conflict, "Lånet är redan återlämnat.");
}
