using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.LendingPolicies;

public static class LendingPolicyErrors
{
    public static Error LoanDaysInvalid =>
        new(ErrorType.Validation, "Lånetiden måste vara mellan 1 och 365 dagar.");
}
