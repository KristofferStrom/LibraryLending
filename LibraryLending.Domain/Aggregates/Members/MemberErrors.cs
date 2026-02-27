using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Members;

public static class MemberErrors
{
    public static Error Inactive =>
        new(ErrorType.Conflict, "Medlemmen är inte aktiv.");

    public static Error AlreadyBorrowingSameBook =>
    new(ErrorType.Conflict, "Medlemmen lånar redan ett exemplar av samma bok.");

}
