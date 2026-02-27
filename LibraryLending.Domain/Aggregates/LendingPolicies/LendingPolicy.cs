using LibraryLending.SharedKernel.Results;

namespace LibraryLending.Domain.Aggregates.LendingPolicies;

public sealed class LendingPolicy
{
    public LendingPolicyId Id { get; }
    public int LoanDays { get; private set; }
    public bool IsActive { get; private set; }

    private LendingPolicy(LendingPolicyId id, int loanDays, bool isActive) =>
        (Id, LoanDays, IsActive) = (id, loanDays, isActive);

    public static Result<LendingPolicy> Create(int loanDays)
    {
        if (loanDays < 1 || loanDays > 365)
            return LendingPolicyErrors.LoanDaysInvalid;

        return new LendingPolicy(LendingPolicyId.New(), loanDays, isActive: true);
    }

    internal static LendingPolicy Rehydrate(LendingPolicyId id, int loanDays, bool isActive) =>
        new(id, loanDays, isActive);

    public Result ChangeLoanDays(int newLoanDays)
    {
        if (newLoanDays < 1 || newLoanDays > 365)
            return LendingPolicyErrors.LoanDaysInvalid;

        LoanDays = newLoanDays;
        return Result.Success();
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
