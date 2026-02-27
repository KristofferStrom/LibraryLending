using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.Members;

public static class MemberErrors
{
    public static Error Inactive =>
        new(ErrorType.Conflict, "Medlemmen är inte aktiv.");
}
