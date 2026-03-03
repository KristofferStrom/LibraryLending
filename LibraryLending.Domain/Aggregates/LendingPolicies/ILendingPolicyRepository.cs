namespace LibraryLending.Domain.Aggregates.LendingPolicies;

public interface ILendingPolicyRepository
{
    Task<LendingPolicy?> GetCurrentAsync(CancellationToken ct);
}
