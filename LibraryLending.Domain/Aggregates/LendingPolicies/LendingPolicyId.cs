namespace LibraryLending.Domain.Aggregates.LendingPolicies;

public readonly record struct LendingPolicyId(Guid Value)
{
    public static LendingPolicyId New() => new(Guid.NewGuid());

    public override string ToString() => Value.ToString();
}
